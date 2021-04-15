using System;
using System.Collections.Generic;
using System.Linq;
using Catalog.Dtos;
using Catalog.Entities;
using Catalog.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Controllers
{
    //Controllers extends the base controller class


    [ApiController]                     //adds aditional default behaviours with [ApiController] attribute
    [Route("[controller]")]            // route is the name of the controler  "/items" can explicitly call it [Route("items")] 
    public class ItemsController : ControllerBase
    {
        private readonly IItemsRepository repository;

        public ItemsController(IItemsRepository repository)       //Dependency injection using interface to pass repository
        {
            this.repository = repository;
        }

        //GET /items
        [HttpGet]              //specify http verb
        public IEnumerable<ItemDto> GetItems()
        {
            var items = repository.GetItems().Select(item => item.AsDto());
            return items;
        }

        //GET /items/{id}
        [HttpGet("{id}")]       // Get tmemplate
        public ActionResult<ItemDto> GetItem(Guid id)
        {
            var item = repository.GetItem(id);

            if (item is null)
            {
                return NotFound();                                                               //because this doesnt return an Item we need to use ActionResult - which alows return more types
            }
            return item.AsDto();
        }

        //POST /items
        // convention for POST is to return the created object
        [HttpPost]
        public ActionResult<ItemDto> CreateItem(CreateItemDto itemDto)                               //input contract
        {
            Item item = new()
            {
                Id = Guid.NewGuid(),
                Name = itemDto.Name,
                Price = itemDto.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };

            repository.CreateItem(item);                                                            //create the item from the object above

            return CreatedAtAction(nameof(GetItem), new { Id = item.Id }, item.AsDto());            //can use CreatedAtRoute, this also returns the header
        }

        //PUT /items
        [HttpPut("{id}")]
        //convention for PUT is to not return anything - no content
        public ActionResult UpdateItem(Guid id, UpdateItemDto itemDto)                               //now Action result doesnt return anything so dont need any type <>
        {
            var existingItem = repository.GetItem(id);

            if (existingItem is null)
            {
                return NotFound();
            }

            Item updatedItem = existingItem with                                                      //with expression for RECORDs ITEM(dtos) we create copy of existingItem and modify the desired values
            {
                Name = itemDto.Name,
                Price = itemDto.Price
            };

            repository.UpdateItem(updatedItem);

            return NoContent();                                                                     // return Http code 204
        }

        //DELETE /items/{id}
        //similar to Updating this doesnt return nothing
        [HttpDelete("{id}")]
        public ActionResult DeleteItem(Guid id)
        {
            var existingItem = repository.GetItem(id);

            if (existingItem is null)
            {
                return NotFound();
            }

            repository.DeleteItem(id);

            return NoContent();

        }
    }
}
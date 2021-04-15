using Catalog.Dtos;
using Catalog.Entities;

namespace Catalog
{
    //extension method must be static
    public static class Extensions
    {
        public static ItemDto AsDto(this Item item)         //operates on current item - this
        {
            return new ItemDto
            {
                Id = item.Id,
                Name = item.Name,
                Price = item.Price,
                CreatedDate = item.CreatedDate
            };
        }
    }
}
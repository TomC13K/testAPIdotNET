using System.ComponentModel.DataAnnotations;        //used for "required" and price range attributes

namespace Catalog.Dtos
{
    //need new Dto to CreateItem because ItemDto have all fields which i dont need - ID and Date which dont need to set when creating the item
    public record CreateItemDto
    {
        [Required]                          //so we dont get null if POST empty field -data validation
        public string Name { get; init; }
        
        [Required]
        [Range(1,1000)]
        public decimal Price { get; init; }
    }
}
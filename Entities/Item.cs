using System;

namespace Catalog.Entities
{
    /*
    Record types  (instead of a class) .NET 5
    - better support for immutale data
    - with expression support
    - value based equality support
    */
    public record Item
    {
        //init - C#9 set value for property initialiser only during the initialisation = immutable property
        //     - better than ..private set.. bcause don't need to  use a constructor to access it
        //set - can modify the value anytime 
        public Guid Id { get; init; }
        public string Name { get; init; }
        public decimal Price { get; init; }
        public DateTimeOffset CreatedDate { get; init; } 
    }
}
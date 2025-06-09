using System;
using System.ComponentModel.DataAnnotations;

namespace Resume_Blazor.Models // Recommended: Use a 'Models' namespace
{
    // Represents a single item in our table
    public class Item
    {
        // Unique identifier for each item
        public Guid Id { get; set; } = Guid.NewGuid();
        // Name property with validation
        [Required] public string Name { get; set; } = string.Empty;
        // Age property with range validation
        [Range(0, 150)] public int Age { get; set; }

        // Creates a new Item instance with the same property values.
        // This is crucial for editing so we don't modify the original item directly
        // until SaveEdit is called.
        public Item Clone() => new Item { Id = this.Id, Name = this.Name, Age = this.Age };
    }
}

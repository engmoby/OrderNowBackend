using System.Collections.Generic;

namespace E_Guest.API.Models
{
    public class CategoryModel
    {
        public long CategoryId { get; set; } 
        public long RestaurantId { get; set; } 
        public Dictionary<string, string> CategoryNameDictionary { get; set; }
        public bool IsActive { get; set; }
        public string ImageURL { get; set; } 
        public bool IsImageChange { get; set; }
        public virtual ICollection<ItemModel> Items { get; set; }

    }
}
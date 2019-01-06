using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderNow.DAL.Entities.Model;
using Repository.Pattern.Ef6;

namespace OrderNow.DAL.Entities.Model
{
    public class Category : Entity
    {
        public Category()
        {
            CategoryTranslations = new List<CategoryTranslation>();
            Items = new List<Item>(); 
        }
        public long CategoryId { get; set; }
        public virtual ICollection<CategoryTranslation> CategoryTranslations { get; set; }
        [ForeignKey("Restaurant")]
        public long RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; }
        public virtual ICollection<Item> Items { get; set; } 
        public bool IsActive { get; set; }
        public int TenantId { get; set; }

        public bool IsDeleted { get; set; }
         
    }
}

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Repository.Pattern.Ef6;

namespace OrderNow.DAL.Entities.Model
{
    public class Restaurant : Entity
    {
        public Restaurant()
        {
            RestaurantTranslations = new List<RestaurantTranslation>();
            //   SideItems = new List<SideItem>();
            Sizes = new List<Size>();
            RestaurantWaiters = new List<RestaurantWaiter>();
            Branches = new List<Branch>();
            FeedBacks = new List<FeedBack>();
            Countries = new List<Country>();
            Categories = new List<Category>();
        }
        public long RestaurantId { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<RestaurantTranslation> RestaurantTranslations { get; set; }
        public virtual ICollection<Country> Countries { get; set; }
        public virtual ICollection<Category> Categories { get; set; }

        [ForeignKey("RestaurantAdmin")]
        public long RestaurantAdminId { get; set; }
        public virtual RestaurantAdmin RestaurantAdmin { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsReady { get; set; }
        //   public virtual ICollection<SideItem> SideItems { get; set; }
        public virtual ICollection<Size> Sizes { get; set; }

        public virtual ICollection<RestaurantWaiter> RestaurantWaiters { get; set; }

        [ForeignKey("Background")]
        public long BackgroundId { get; set; }
        public virtual Background Background { get; set; }
        public virtual ICollection<Branch> Branches { get; set; }
        [ForeignKey("Package")]
        public long? PackageId { get; set; }
        public virtual Package Package { get; set; }

        [ForeignKey("Admin")]
        public long AdminId { get; set; }
        public virtual Admin Admin { get; set; }

        public virtual ICollection<FeedBack> FeedBacks { get; set; }
        public int TenantId { get; set; }

    }
}

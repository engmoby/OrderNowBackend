using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Repository.Pattern.Ef6;

namespace OrderNow.DAL.Entities.Model
{
    public class Country:Entity
    {
        public Country()
        {
            CountryTranslations = new List<CountryTranslation>();
            Regions = new List<Region>(); 
        }
        public long CountryId { get; set; }
        [ForeignKey("Restaurant")]
        public long RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public DateTime? CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public long? DeleterUserId { get; set; }
        public int TenantId { get; set; }
        public virtual ICollection<CountryTranslation> CountryTranslations { get; set; }
        public virtual ICollection<Region> Regions { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Repository.Pattern.Ef6;

namespace OrderNow.DAL.Entities.Model
{
    public class Region:Entity
    {
        public Region()
        {
            RegionTranslations = new List<RegionTranslation>();
            Cities = new List<City>();
        }
        public long RegionId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public DateTime? CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public long? DeleterUserId { get; set; }
        public virtual ICollection<RegionTranslation> RegionTranslations { get; set; }
        public virtual ICollection<City> Cities { get; set; }
        [ForeignKey("Country")]
        public long CountryId { get; set; }
        public virtual Country Country { get; set; }
    }
}

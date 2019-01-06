using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OperationSurvey.API.Models
{
    public class CountryModel
    {
        public long CountryId { get; set; }
        public Dictionary<string, string> TitleDictionary { get; set; }
        public long RestaurantId { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public DateTime? CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public long? DeleterUserId { get; set; }
    }
}
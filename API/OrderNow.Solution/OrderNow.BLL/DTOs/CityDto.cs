using System;
using System.Collections.Generic;

namespace OrderNow.BLL.DTOs
{
    public class CityDto
    {
        public long CityId { get; set; }
        public Dictionary<string, string> TitleDictionary { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public DateTime? CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public long? DeleterUserId { get; set; }
        public long RegionId { get; set; }
        public RegionDto Region { get; set; }
        public Dictionary<string, string> RegionNameDictionary { get; set; }
    }
}

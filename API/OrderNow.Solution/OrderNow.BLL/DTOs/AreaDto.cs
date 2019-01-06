using System;
using System.Collections.Generic;

namespace OrderNow.BLL.DTOs
{
    public class AreaDto
    {
        public long AreaId { get; set; }
        public Dictionary<string, string> TitleDictionary { get; set; }
 
        public bool IsStatic { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public DateTime? CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public long? DeleterUserId { get; set; }
        public List<BranchDto> Branches { get; set; }

        public long CityId { get; set; }
        public CityDto City { get; set; }

    }
}


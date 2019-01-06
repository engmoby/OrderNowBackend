using System;
using System.Collections.Generic;

namespace OrderNow.BLL.DTOs
{
    public class BranchDto
    {
        public long BranchId { get; set; }
        public Dictionary<string, string> TitleDictionary { get; set; }
        public long AreaId { get; set; }
        public AreaDto Area { get; set; }
        public List<TableDto> Tables { get; set; }

        public bool IsStatic { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public DateTime? CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public long? DeleterUserId { get; set; }
    }
}


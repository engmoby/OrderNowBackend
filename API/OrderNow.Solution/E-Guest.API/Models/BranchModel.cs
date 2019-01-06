using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OperationSurvey.API.Models;

namespace E_Guest.API.Models
{
    public class BranchModel
    {
        public long BranchId { get; set; }
        public Dictionary<string, string> TitleDictionary { get; set; }
        public long AreaId { get; set; }
        public AreaModel Area { get; set; }
        public List<TableModel> Tables { get; set; }

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
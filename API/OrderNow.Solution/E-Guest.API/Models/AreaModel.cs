using System;
using System.Collections.Generic;
using E_Guest.API.Models;

namespace OperationSurvey.API.Models
{
    public class AreaModel
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
        public List<BranchModel> Branches { get; set; }
        public long CityId { get; set; }
        public CityModel City { get; set; }

    }
}

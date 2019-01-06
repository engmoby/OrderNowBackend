using System.Collections.Generic;
using OrderNow.Common;

namespace E_Guest.API.Models
{
    public class FeatureControlDetailModel
    {
        public long FeatureControlId { get; set; }
        public int Order { get; set; }
        public long FeatureId { get; set; }
        public Enums.Control Control { get; set; }

        public string ControlType { get; set; }
        public List<FeatureDetailModel> FeatureDetails { get; set; }
    }
}
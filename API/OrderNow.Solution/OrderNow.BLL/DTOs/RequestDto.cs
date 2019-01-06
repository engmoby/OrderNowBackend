using System;
using System.Collections.Generic;
using OrderNow.Common;

namespace OrderNow.BLL.DTOs
{
    public class RequestDto
    {
        public string LogoURL;
        public long RequestId { get; set; }
        public long UserId { get; set; }
        public string UserName { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime ModifyTime { get; set; }
        public string Modifier { get; set; }
        public long ModifyBy { get; set; } 
        public Enums.RequestStatus Status { get; set; }
        public Enums.FeatureType Type { get; set; }

        public long? RestaurantId { get; set; }
        public long? TableId { get; set; }
        public string TableName { get; set; }

        public Dictionary<string, string> RestaurantName { get; set; }
        public List<RequestDetailDto> RequestDetails { get; set; }
        public string Comment { get; set; }
        public DateTime? RequestTime { get; set; }
        public TableDto Table { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Guest.API.Models
{
    public class RequestModel
    {
        public string LogoURL;

        public long RequestId { get; set; }
        //public long RoomId { get; set; }
        //public string RoomName { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime ModifyTime { get; set; }
        public string Modifier { get; set; }
        public long ModifyBy { get; set; } 
        public string Status { get; set; }
        public string Type { get; set; }
        public long? RestaurantId { get; set; }
        public long? TableId { get; set; }
       public string TableName { get; set; }
        public long UserId { get; set; }

        public Dictionary<string, string> RestaurantName { get; set; }
        public List<RequestDetailModel> RequestDetails { get; set; }
        public string Comment { get; set; }
        public DateTime? RequestTime { get; set; }
    }
}
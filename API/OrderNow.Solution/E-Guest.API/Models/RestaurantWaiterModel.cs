using System.Collections.Generic;

namespace E_Guest.API.Models
{
    public class RestaurantWaiterModel
    {
        public long UserId { get; set; }
        public string Phone { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public Dictionary<string, string> BranchTitleDictionary { get; set; }
        public long BranchId { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
    }
}
using System.Collections.Generic;

namespace OrderNow.BLL.DTOs
{
    public class RestaurantWaiterDto
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
        //public long RestaurantId { get; set; }
    }
}

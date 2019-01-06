using System.Collections.Generic;

namespace OrderNow.API.Models
{
    public class SupervisorModel
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public List<RestaurantModel> Restaurant { get; set; }
    }
}
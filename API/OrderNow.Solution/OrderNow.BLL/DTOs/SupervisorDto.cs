using System.Collections.Generic;

namespace OrderNow.BLL.DTOs
{
    public class SupervisorDto
    {
        public long UserId { get; set; }
        public string  Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
      //  public List<RestaurantDTO> Restaurant { get; set; }
        public long BranchId { get; set; }
        public string Phone { get; set; }
    }
}

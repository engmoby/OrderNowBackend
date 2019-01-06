using OrderNow.Common;

namespace OrderNow.BLL.DTOs
{
    public class UserDto
    {
        public long UserId { get; set; }
        public long TenantId { get; set; }
        public long RestaurantId { get; set; }

        public string Email { get; set; }
        public string Phone { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Enums.RoleType Role { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using OrderNow.Common;
using Repository.Pattern.Ef6;

namespace OrderNow.DAL.Entities.Model
{
    public class User:Entity
    {
        [Key]
        public long UserId { get; set; }
        public long RestaurantId { get; set; }

        public string Email { get; set; }
        public string Phone{ get; set; }
        public string UserName { get; set; } 
        public string Password { get; set; } 
        public Enums.RoleType Role { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsActive { get; set; }
        public int TenantId { get; set; }

    }
}

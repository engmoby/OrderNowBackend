using System.Web.Http;
using OrderNow.Common;

namespace E_Guest.API.Providers
{
    public class AuthorizeRolesAttribute : AuthorizeAttribute
    {
        private Enums.RoleType restaurantAdmin;

        public AuthorizeRolesAttribute(Enums.RoleType restaurantAdmin)
        {
            this.restaurantAdmin = restaurantAdmin;
        }

        public AuthorizeRolesAttribute(params string[] roles) : base()
        {
            Roles = string.Join(",", roles);
        }
        public AuthorizeRolesAttribute(params Enums.RoleType[] roles) : base()
        {
            Roles = string.Join(",", roles);
        }
    }
}
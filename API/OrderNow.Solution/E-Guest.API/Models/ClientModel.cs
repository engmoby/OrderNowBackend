using System.Collections.Generic;

namespace E_Guest.API.Models
{
    public class ClientModel
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string ClientName { get; set; }

    }
}
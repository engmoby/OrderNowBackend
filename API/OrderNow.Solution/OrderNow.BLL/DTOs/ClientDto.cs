using System.Collections.Generic;

namespace OrderNow.BLL.DTOs
{
    public class ClientDto
    {
        public long UserId { get; set; }
        public string Phone { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ClientName { get; set; } 
    }
}

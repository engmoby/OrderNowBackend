using System.Collections.Generic;
using OrderNow.BLL.DTOs;

namespace E_Guest.API.Models
{
    public class ItemOrderModel
    {
        public List<ItemNamesDto> ItemNames{ get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OrderNow.BLL.DTOs;

namespace OrderNow.API.Models
{
    public class ItemOrderModel
    {
        public List<ItemNamesDto> ItemNames{ get; set; }
    }
}
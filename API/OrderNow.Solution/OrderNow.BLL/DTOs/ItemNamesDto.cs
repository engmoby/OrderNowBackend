﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderNow.BLL.DTOs
{
    public class ItemNamesDto
    {
        public long ItemId { get; set; }
        public string ItemName { get; set; }
        public int OrderNumber { get; set; }
    }
}

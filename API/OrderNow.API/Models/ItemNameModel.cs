﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderNow.API.Models
{
    public class ItemNameModel
    {
        public long ItemId { get; set; }
        public string ItemName { get; set; }
        public int OrderNumber { get; set; }

    }
}
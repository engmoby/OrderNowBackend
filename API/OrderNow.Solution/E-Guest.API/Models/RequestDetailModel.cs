﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Guest.API.Models
{
    public class RequestDetailModel
    {
        public long RequestDetailId { get; set; } 
        public int Number { get; set; }
        public Dictionary<string, string> DescriptionDictionary { get; set; }
        public decimal Price { get; set; }
        public long? ItemSizeId { get; set; }
        public long ItemId { get; set; }

        //public bool IsFree { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
    }
}
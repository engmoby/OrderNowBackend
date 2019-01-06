using System;
using System.Collections.Generic;

namespace OrderNow.BLL.DTOs
{
    public class SellerItemDto
    {
        public string ImageUrl;
        public long ItemId { get; set; }
        public Dictionary<string, string> TitleDictionary { get; set; }
        public ItemDTO Item { get; set; }
        public int Count { get; set; }

    }
}


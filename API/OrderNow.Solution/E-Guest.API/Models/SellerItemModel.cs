using System;
using System.Collections.Generic;
using E_Guest.API.Models;

namespace OperationSurvey.API.Models
{
    public class SellerItemModel
    {
        public long ItemId { get; set; }
        public Dictionary<string, string> TitleDictionary { get; set; }
        public string ImageUrl;
        public ItemModel Item { get; set; }

        public int Count { get; set; }

    }
}

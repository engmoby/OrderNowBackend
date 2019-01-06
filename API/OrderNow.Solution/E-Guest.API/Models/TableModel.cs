using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Guest.API.Models
{
    public class TableModel
    {
        public long TableId { get; set; }
        public string TableName { get; set; }

        public long BranchId { get; set; }
    }
}
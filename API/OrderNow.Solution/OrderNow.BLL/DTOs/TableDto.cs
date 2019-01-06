using System;
using System.Collections.Generic;

namespace OrderNow.BLL.DTOs
{
    public class TableDto
    {
        public long TableId { get; set; } 
        public string TableName { get; set; }

        public long BranchId { get; set; }
        public BranchDto Branch { get; set; }
    }
}


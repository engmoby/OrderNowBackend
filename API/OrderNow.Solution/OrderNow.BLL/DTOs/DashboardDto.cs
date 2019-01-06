using System.Collections.Generic;

namespace OrderNow.BLL.DTOs
{
    public class DashboardDto
    {

        public Dictionary<string, string> XaxisName { get; set; }
        public long PendingCount { get; set; }
        public long ApprovedCount { get; set; }  
        public long RejectedCount { get; set; } 
    }
}


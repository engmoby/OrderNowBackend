using OrderNow.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderNow.Common;

namespace OrderNow.BLL.DTOs
{
    public class RequestStatusDto
    {
        public long RequestId { get; set; }
        public long RoomId { get; set; }
        public string RoomName { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime ModifyTime { get; set; }
        public Enums.RequestStatus Status { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderNow.DAL.Entities.Model;
using Repository.Pattern.Ef6;

namespace OrderNow.DAL.Entities.Model
{
    public class FeedBack:Entity
    {
        public long FeedBackId { get; set; }
        public string Comment { get; set; }
        public int Rate { get; set; }
        //[ForeignKey("Restaurant")]
        //public long RestaurantId { get; set; }
        //public virtual Restaurant Restaurant { get; set; }

        [ForeignKey("Branch")]
        public long BranchId { get; set; }
        public virtual Branch Branch { get; set; }

     

        [Column(TypeName = "datetime2")]
        public DateTime CreateTime { get; set; }
        public int TenantId { get; set; }
    }
}

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
    public class RestaurantWaiter: User
    {
        public string Name { get; set; }

        //[ForeignKey("Restaurant")]
        //public long RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; }

        public long BranchId { get; set; }
        public virtual Branch Branch { get; set; }
        //[ForeignKey("Package")]
        //public long PackageId { get; set; }
        //public virtual Package Package { get; set; }
    }
}

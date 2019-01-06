using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderNow.DAL.Entities.Model
{
    public class Supervisor : User
    {
        public Supervisor()
        {
            SupervisorFeatures = new List<SupervisorFeature>();
            Requests = new List<Request>();
          //  RestaurantAdmins = new List<RestaurantAdmin>();
        }
        [ForeignKey("Admin")]
        public long AdminId { get; set; }
        public virtual Admin Admin { get; set; }

        //[ForeignKey("Feature")]
        //public long FeatureId { get; set; }
        //public virtual Feature Feature { get; set; }
        public virtual List<SupervisorFeature> SupervisorFeatures { get; set; }
        public virtual List<Request> Requests { get; set; }
     //   public virtual List<RestaurantAdmin> RestaurantAdmins { get; set; }

    }
}

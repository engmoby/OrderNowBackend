using System.ComponentModel.DataAnnotations.Schema;
using Repository.Pattern.Ef6;

namespace OrderNow.DAL.Entities.Model
{
    public class SupervisorFeature:Entity
    {
        public long SupervisorFeatureId { get; set; }

        [ForeignKey("Restaurant")]
        public long RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; }


      //  [ForeignKey("Supervisor")]
        public long SupervisorId { get; set; }
     //   public virtual Supervisor Supervisor { get; set; }
    }
}

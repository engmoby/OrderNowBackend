using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderNow.DAL.Entities.Model;
using Repository.Pattern.Ef6;

namespace OrderNow.DAL.Entities.Model
{
    public class RestaurantAdmin: User
    {

        //[ForeignKey("Restaurant")]
     //   public long RestaurantId { get; set; }
       // public virtual Restaurant Restaurant { get; set; }
    }
}

using System.ComponentModel.DataAnnotations.Schema;
using Repository.Pattern.Ef6;

namespace OrderNow.DAL.Entities.Model
{
    public class Table : Entity
    {  
        public long TableId { get; set; } 
        public string TableName { get; set; }
        public virtual Request Request { get; set; }

        [ForeignKey("Branch")]
        public long BranchId { get; set; }
        public virtual Branch Branch { get; set; }

    }
}

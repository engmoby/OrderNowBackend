using System;
using System.Collections.Generic;
using OrderNow.DAL.Entities.Model;

namespace OrderNow.DAL.Entities.Model
{
    public class Admin : User
    {
        public Admin()
        {
            Supervisors = new List<Supervisor>();
            Restaurants = new List<Restaurant>();
            Packages = new List<Package>();
        }
        public Guid UserAccountId { get; set; }
        public long ProductId { get; set; }
        public virtual ICollection<Package> Packages { get; set; }
        public virtual ICollection<Restaurant> Restaurants { get; set; }
        public virtual List<Supervisor> Supervisors { get; set; }

    }
}

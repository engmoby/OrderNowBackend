﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Guest.API.Models
{
    public class SupervisorModel
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string  Name { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        //  public List<FeatureModel> Features { get; set; }
        public long BranchId { get; set; }

    }
}
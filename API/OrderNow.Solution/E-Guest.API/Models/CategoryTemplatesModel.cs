﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Guest.API.Models
{
    public class CategoryTemplatesModel
    {
        public long CategoryId { get; set; }
        public List<PageModel> PageModels { get; set; }
    }
}
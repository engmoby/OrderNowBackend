﻿using System;
using OrderNow.Common;

namespace E_Guest.API.Models
{
    public class AvailableModel
    {
        public long AvailableId { get; set; }
        public DateTime From { get; set; }

        public DateTime To { get; set; }

        public long FeatureDetailId { get; set; }

        public Enums.Day Day { get; set; }
        public int Max { get; set; }
    }
}
﻿using Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models.QueryModels
{
    public class TollFeeQueryData
    {
        [Required]
        public VehicleBase Vehicle { get; set; }
        [Required]
        public DateTime[] Dates { get; set; }
    }
}

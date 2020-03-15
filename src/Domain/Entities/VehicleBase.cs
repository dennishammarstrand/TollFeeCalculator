using Domain.Enums;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class VehicleBase : IVehicle
    {
        public VehicleTypes Type { get; set; }
        public VehicleTypes GetVehicleType()
        {
            return Type;
        }
    }
}

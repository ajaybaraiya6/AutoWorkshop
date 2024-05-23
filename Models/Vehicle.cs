using System;
using System.Collections.Generic;

namespace AutoWorkshop.Models
{
    public partial class Vehicle
    {
        public Vehicle()
        {
            VehicleServices = new HashSet<VehicleService>();
        }

        public string VehicleNumber { get; set; } = null!;
        public int? CustomerId { get; set; }
        public string? VehicleMake { get; set; }
        public string? VehicleModel { get; set; }
        public int? VehicleYear { get; set; }

        public virtual Customer? Customer { get; set; }
        public virtual ICollection<VehicleService> VehicleServices { get; set; }
    }
}

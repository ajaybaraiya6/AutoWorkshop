using System;
using System.Collections.Generic;

namespace AutoWorkshop.Models
{
    public partial class VehicleService
    {
        public int ServiceId { get; set; }
        public string? VehicleNumber { get; set; }
        public DateTime? ServiceDate { get; set; }
        public int? SlotId { get; set; }

        public virtual Slot? Slot { get; set; }
        public virtual Vehicle? VehicleNumberNavigation { get; set; }
    }
}

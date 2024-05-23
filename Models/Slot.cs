using System;
using System.Collections.Generic;

namespace AutoWorkshop.Models
{
    public partial class Slot
    {
        public Slot()
        {
            VehicleServices = new HashSet<VehicleService>();
        }

        public int SlotId { get; set; }
        public int? AssignedHours { get; set; }

        public virtual ICollection<VehicleService> VehicleServices { get; set; }
    }
}

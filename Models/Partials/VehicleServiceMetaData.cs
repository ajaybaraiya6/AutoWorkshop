using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AutoWorkshop.Models
{
    public class VehicleServiceMetaData
    {
        [Required(ErrorMessage = "Vehicle Number is required.")]
        [DisplayName("Vehicle Number")]
        public string? VehicleNumber { get; set; }
        [Required(ErrorMessage = "Service Date is required.")]
        [DisplayName("Service Date")]
        public DateTime? ServiceDate { get; set; }
        [Required(ErrorMessage = "Slot is required.")]
        [DisplayName("Slot")]
        public int? SlotId { get; set; }
    }
}

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AutoWorkshop.Models
{
    public class VehicleMetadata
    {
        [Required]
        [DisplayName("Vehicle Number")]
        [MaxLength(10)]
        public string VehicleNumber { get; set; } = null!;
        
        [DisplayName("Vehicle Make")]
        [MaxLength(20)]
        public string? VehicleMake { get; set; }
        
        [DisplayName("Vehicle Model")]
        [MaxLength(20)]
        public string? VehicleModel { get; set; }
        
        [DisplayName("Vehicle Year")]
        public int? VehicleYear { get; set; }
    }
}

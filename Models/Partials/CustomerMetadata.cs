using Microsoft.CodeAnalysis;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AutoWorkshop.Models
{
    public class CustomerMetadata
    {
        [MaxLength(20)]
        [DisplayName("First Name")] 
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "At least Last Name is required.")]
        [MaxLength(20)]
        [DisplayName("Last Name")] 
        public string? LastName { get; set; }

        [MaxLength(200)]
        [DisplayName("Customer Address")] 
        public string? CustomerAddress { get; set; }

    }
}

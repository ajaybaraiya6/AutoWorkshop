using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace AutoWorkshop.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Vehicles = new HashSet<Vehicle>();
        }

        public int CustomerId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? CustomerAddress { get; set; }

        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}

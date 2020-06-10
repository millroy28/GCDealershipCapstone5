using System;
using System.Collections.Generic;

namespace GCDealershipCapstone.Models
{
    public partial class Inventory
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public string Make { get; set; }
        public string Style { get; set; }
        public int? Year { get; set; }
        public string Color { get; set; }
        public int? Mileage { get; set; }
        public bool? Transmission { get; set; }
    }
}

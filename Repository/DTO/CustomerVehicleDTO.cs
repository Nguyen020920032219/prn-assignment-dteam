using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DTO
{
    public class CustomerVehicleDTO
    {
        public int CustomerId { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public int VehicleId { get; set; }

        public string VehicleNo { get; set; }

        public string VehicleModel { get; set; }

        public string VehicleMake { get; set; }

        public int? VehicleYear { get; set; }
    }
}

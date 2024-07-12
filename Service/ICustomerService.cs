using Repository.DTO;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface ICustomerService
    {
        List<Customer> GetAllCustomers();

        List<CustomerVehicleDTO> GetAllCustomersWithVehicles();
    }
}

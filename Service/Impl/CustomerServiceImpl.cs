using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.DTO;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Impl
{
    public class CustomerServiceImpl : ICustomerService
    {
        private RepositoryBase<Customer> repository;
        private CarWashContext _context;

        public List<Customer> GetAllCustomers()
        {
            repository = new RepositoryBase<Customer>();
            return repository.GetAll();
        }

        public List<CustomerVehicleDTO> GetAllCustomersWithVehicles()
        {
            _context = new CarWashContext();

            var query = from c in _context.Customers
                        join v in _context.Vehicles on c.CustomerId equals v.CustomerId
                        select new CustomerVehicleDTO
                        {
                            CustomerId = c.CustomerId,
                            Name = c.Name,
                            Phone = c.Phone,
                            Address = c.Address,
                            VehicleId = v.VehicleId,
                            VehicleNo = v.LicensePlate,
                            VehicleMake = v.Make,
                            VehicleModel = v.Model,
                            VehicleYear = v.Year
                        };

            return query.ToList();
        }
    }
}

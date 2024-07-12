using System;
using Repository;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.DTO;

namespace Service
{
    public class CustomerService
    {
        CustomerRepository _customerRepo;
        private CarWashContext _context;

        public CustomerService()
        {
            _customerRepo = new CustomerRepository();
        }

        public List<Customer> GetCustomers()
        {
            return _customerRepo.GetAll();
        }

        public Customer GetCustomerById(int id)
        {
            return _customerRepo.GetAll().FirstOrDefault(c => c.CustomerId == id);
        }

        public List<Customer> GetCustomersContainString(string txtSearch)
        {
            List<Customer> customers = _customerRepo.GetAll();
            List<Customer> result = new List<Customer>();
            foreach (Customer customer in customers)
            {
                if (customer.Name.ToLower().Contains(txtSearch.ToLower()))
                {
                    result.Add(customer);
                }
            }
            return result;
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

        public void DeleteCustomer(Customer customer)
        {
            _customerRepo.Delete(customer);
        }
        public void UpdateCustomer(Customer customer)
        {
            _customerRepo.Update(customer);
        }

        public void CreateCustomer(Customer customer)
        {
            _customerRepo.Add(customer);
        }
    }
}

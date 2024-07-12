using System;
using Repository;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Entities;

namespace Service
{
    public class CustomerService
    {
        CustomerRepository _customerRepo;

        public CustomerService()
        {
            _customerRepo =new CustomerRepository();
        }

        public List<Customer> GetCustomers()
        {
            return _customerRepo.GetAll();
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

using Repository;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class EmployeeService
    {
        EmployeeRepository _employeeRepository;

        AccountRepository _accountRepository;
        public EmployeeService()
        {
            _employeeRepository = new EmployeeRepository();
            _accountRepository = new AccountRepository();
        }
        public List<Employee> GetEmployees()
        {
            return _employeeRepository.GetAll();
        }

        public List<Employee> GetAllEmployees()
        {
            List<Employee> employees = _employeeRepository.GetAll();
            List<Employee> result = new List<Employee>();
            foreach (Employee employee in employees)
            {
                if (employee.IsActive == true)
                {
                    result.Add(employee);
                }
            }
                return result;
        }

        public List<Employee> GetEmployeeByName(String txtName)
        {
            List<Employee> employees = _employeeRepository.GetAll();
            List<Employee> result = new List<Employee>();
            foreach (Employee employee in employees)
            {
                if (employee.Name.ToLower().Contains(txtName.ToLower()))
                {
                    result.Add(employee);
                }
            }

            return result;
        }

        public void DeleteEmployee(Employee employee)
        {
            employee.IsActive = false;
            foreach (var account in employee.Accounts)
            {
                account.IsActive = false;
                _accountRepository.Update(account);
            }
            _employeeRepository.Update(employee);
        }

        public void UpdateEmployee(Employee employee)
        {
            
            _employeeRepository.Update(employee);
        }

        public void CreateEmployee(Employee employee)
        {
            _employeeRepository.Add(employee);
        }


    }
    
}

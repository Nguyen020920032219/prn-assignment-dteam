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
        public EmployeeService()
        {
            _employeeRepository = new EmployeeRepository();
        }
        public List<Employee> GetEmployees()
        {
            return _employeeRepository.GetAll();
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
            _employeeRepository.Delete(employee);
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

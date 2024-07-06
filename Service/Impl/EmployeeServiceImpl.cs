using Repository;
using Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace Service.Impl
{
    public class EmployeeServiceImpl : IEmployeeService
    {
        private CarWashContext _context;

        public void AddEmployee(TbEmployee employee)
        {
            _context = new CarWashContext();

            _context.TbEmployees.Add(employee);
            _context.SaveChanges();
        }

        public List<TbEmployee> GetTbEmployeeContainNameList(string name)
        {
            _context = new CarWashContext();

            return _context.TbEmployees.Where(employee =>  employee.Name.ToLower().Contains(name.ToLower().Trim())).ToList(); 
        }

        public List<TbEmployee> GetTbEmployeesList()
        {
            _context = new CarWashContext();

            return _context.TbEmployees.ToList();
        }

        public TbEmployee? GetTbEmployee(string name, string password)
        {
            _context = new CarWashContext();

            return _context.TbEmployees.FirstOrDefault(employee => employee.Name.Equals(name) && employee.Password.Equals(password));
        }

        public void UpdateEmployee(TbEmployee employee)
        {
            _context = new CarWashContext();

            var tracker = _context.TbEmployees.Attach(employee);
            tracker.State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteEmployee(TbEmployee employee)
        {
            _context = new CarWashContext();

            _context.TbEmployees.Remove(employee);
            _context.SaveChanges();
        }
    }
}

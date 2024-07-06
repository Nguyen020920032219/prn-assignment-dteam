using Repository.Entities;

namespace Service
{
    public interface IEmployeeService
    {
        TbEmployee? GetTbEmployee(string name, string password);

        List<TbEmployee> GetTbEmployeeContainNameList(string name);

        List<TbEmployee> GetTbEmployeesList();

        void AddEmployee(TbEmployee employee);

        void UpdateEmployee(TbEmployee employee);

        void DeleteEmployee(TbEmployee employee);
    }
}

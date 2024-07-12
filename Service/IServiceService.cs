namespace Service
{
    public interface IServiceService
    {
        List<Repository.Entities.Service> GetAllServices();

        public List<Repository.Entities.Service> GetServicesIsNotDeleteList();
    }
}

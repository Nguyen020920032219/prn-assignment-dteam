using Repository;

namespace Service.Impl
{
    public class ServiceServiceImpl : IServiceService
    {
        private RepositoryBase<Repository.Entities.Service> repository;
        private CarWashContext _context;

        public List<Repository.Entities.Service> GetAllServices()
        {
            repository = new RepositoryBase<Repository.Entities.Service>();
            return repository.GetAll();
        }
    }
}

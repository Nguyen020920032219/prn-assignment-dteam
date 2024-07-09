using Repository;

namespace Service.Impl
{
    public class ServiceServiceImpl : IServiceService
    {
        private RepositoryBase<Repository.Entities.Service> _repository;
        private CarWashContext _context;

        public List<Repository.Entities.Service> GetAllServices()
        {
            _repository = new RepositoryBase<Repository.Entities.Service>();
            return _repository.GetAll();
        }
    }
}

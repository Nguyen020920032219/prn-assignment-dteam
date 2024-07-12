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

        public List<Repository.Entities.Service> GetServicesIsNotDeleteList()
        {
            _repository = new ServiceRepository();
            List<Repository.Entities.Service> list = new List<Repository.Entities.Service>();

            try
            {
                List<Repository.Entities.Service> serviceList = _repository.GetAll();


                foreach (Repository.Entities.Service service in serviceList)
                {
                    if (!service.IsDeleted)
                    {
                        list.Add(service);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return list;
        }

        public void addService(Repository.Entities.Service service)
        {
            Boolean result = false;
            try
            {
                _repository.Add(service);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteService(Repository.Entities.Service service)
        {
            try
            {
                service.IsDeleted = true;
                _repository.Update(service);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateService(Repository.Entities.Service service)
        {
            try
            {
                _repository.Update(service);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

using Repository;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ServiceService
    {
        private readonly ServiceRepository _serviceRepository;
        private ValidationService _validationService;
        public ServiceService()
        {
            _serviceRepository = new ServiceRepository();
            _validationService = new ValidationService();
        }
        private List<Repository.Entities.Service> _serviceList;
        public List<Repository.Entities.Service> GetList()
        {
            List<Repository.Entities.Service> list = new List<Repository.Entities.Service>();
            try
            {
                _serviceList = _serviceRepository.GetAll();
                

                foreach (Repository.Entities.Service service in _serviceList)
                {
                    if (!service.IsDiscontinued)
                    {
                        list.Add(service);
                    }
                }
            } catch {
                throw;
            }
            return list;

        }

        public void addService(Repository.Entities.Service service)
        { 
            Boolean result = false;
            try
            {
                _serviceRepository.Add(service);
            } catch{
                throw;
            }
        }

        public void DeleteService(Repository.Entities.Service service)
        {
            try
            {
               _serviceRepository.Update(service);
            } catch{ 
                throw;
            }
        }

        public void UpdateService(Repository.Entities.Service service)
        {
            try
            {
                _serviceRepository.Update(service);
            }
            catch
            {
                throw;
            }
        }

        public List<Repository.Entities.Service> GetServiceContainString(string txtSearch)
        {
            List<Repository.Entities.Service> services = new List<Repository.Entities.Service>();
            List<Repository.Entities.Service> result = new List<Repository.Entities.Service>();

            foreach (Repository.Entities.Service service in _serviceList)
            {
                if (_validationService.IsNumber(txtSearch))
                {
                    if (service.ServiceId.ToString().Contains(txtSearch))
                    {
                        result.Add(service);
                    }
                    
                }
                if (service.Name.ToLower().Contains(txtSearch.ToLower()))
                {
                    result.Add(service);
                }
               
            }
            return result;
        }


    }
}

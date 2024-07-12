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
        ValidationService _validationService;
       
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
            } catch (Exception ex) {
                throw ex;
            }
            return list;
        }

        public List<Repository.Entities.Service> GetServicesByStatus(bool isDiscontinued)
        {
            List<Repository.Entities.Service> services = _serviceRepository.GetAll();
            List<Repository.Entities.Service> result = new List<Repository.Entities.Service>();
            foreach (Repository.Entities.Service service in services)
            {
                if (service.IsDiscontinued == isDiscontinued)
                {
                    result.Add(service);
                }
            }
            return result;
        }

        public List<Repository.Entities.Service> GetAllServices()
        {
            return _serviceRepository.GetAll();
        }

        public void addService(Repository.Entities.Service service)
        { 
            Boolean result = false;
            try
            {
                _serviceRepository.Add(service);
            } catch (Exception ex) {
                throw ex;
            }
        }

        public void DeleteService(Repository.Entities.Service service)
        {
            try
            {
               _serviceRepository.Update(service);
            } catch(Exception ex) { 
                throw ex;
            }
        }

        public void UpdateService(Repository.Entities.Service service)
        {
            try
            {
                _serviceRepository.Update(service);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public List<Repository.Entities.Service> GetServicesContainString(string txtSearch, bool status)
        {
            List<Repository.Entities.Service> services = GetServicesByStatus(status);
            List<Repository.Entities.Service> result = new List<Repository.Entities.Service>();
            foreach (Repository.Entities.Service service in services)
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
                    if (!result.Contains(service))
                    {
                        result.Add(service);
                    }
                }
            }
            return result;
        }
    }
}

using Repository;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace service
{
    public class ServiceService
    {
        private readonly ServiceRepository _serviceRepository;
       
        public ServiceService()
        {
            _serviceRepository = new ServiceRepository();
        }
        private List<Repository.Entities.Service> _serviceList;
        public List<Repository.Entities.Service> GetList()
        {
            List<Service> list = new List<Service>();
            try
            {
                _serviceList = _serviceRepository.GetAll();
                

                foreach (Service service in _serviceList)
                {
                    if (!service.IsDeleted)
                    {
                        list.Add(service);
                    }
                }
            } catch (Exception ex) {
                throw ex;
            }
            return list;

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

        public void DeleteService(Service service)
        {
            try
            {
               _serviceRepository.Update(service);
            } catch(Exception ex) { 
                throw ex;
            }
        }

        public void UpdateService(Service service)
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

        

    }
}

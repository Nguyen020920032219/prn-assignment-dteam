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
       
        public ServiceService()
        {
            _serviceRepository = new ServiceRepository();
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

        

    }
}

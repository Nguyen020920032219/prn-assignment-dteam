using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class OrderServiceService
    {
        private OrderServiceRepository _repository;

        public void AddOrderServiceList(List<Repository.Entities.OrderService> list)
        {
            _repository = new OrderServiceRepository();

            foreach (var item in list)
            {
                _repository.Add(item);
            }
        }

        public void DeleteOrderServicesByOrderId(int orderId)
        {
            _repository = new OrderServiceRepository();

            List<Repository.Entities.OrderService> list = _repository.GetAll()
                                                 .Where(orderService => orderService.OrderId == orderId)
                                                 .ToList();

            foreach (var orderService in list)
            {
                _repository.Delete(orderService);
            }
        }

        public List<Repository.Entities.OrderService> GetOrderServicesByOrderId(int orderId)
        {
            _repository = new OrderServiceRepository();
            return _repository.GetAll()
                              .Where(orderService => orderService.OrderId == orderId)
                                                 .ToList();
        }
    }
}

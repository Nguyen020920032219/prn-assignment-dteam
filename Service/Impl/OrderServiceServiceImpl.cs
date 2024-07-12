using Repository;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Impl
{
    public class OrderServiceServiceImpl : IOrderServiceService
    {
        private OrderServiceRepository _repository;

        public void AddOrderServiceList(List<OrderService> list)
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

            List<OrderService> list = _repository.GetAll()
                                                 .Where(orderService => orderService.OrderId == orderId)
                                                 .ToList();

            foreach (var orderService in list) 
            {
                _repository.Delete(orderService);
            }
        }

        public List<OrderService> GetOrderServicesByOrderId(int orderId)
        {
            _repository = new OrderServiceRepository();
            return _repository.GetAll()
                              .Where(orderService => orderService.OrderId == orderId)
                                                 .ToList();
        }
    }
}

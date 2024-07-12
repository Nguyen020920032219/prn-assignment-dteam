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

        public void addOrderServiceList(List<OrderService> list)
        {
            _repository = new OrderServiceRepository();

            foreach (var item in list)
            {
                _repository.Add(item);
            }
        }
    }
}

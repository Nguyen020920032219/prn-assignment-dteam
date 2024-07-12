using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IOrderServiceService
    {
        void AddOrderServiceList(List<OrderService> list);

        void DeleteOrderServicesByOrderId(int orderId);

        List<OrderService> GetOrderServicesByOrderId(int orderId);
    }
}

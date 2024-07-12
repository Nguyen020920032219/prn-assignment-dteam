using Repository;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Impl
{
    public class OrderServiceImpl : IOrderService
    {
        private OrderRepository _repository;

        public string? GetMaxTransactionNoStartingWithDate(string prefix)
        {
            _repository = new OrderRepository();
            List<Order> orders = _repository.GetAll();

            return  orders
                   .Where(cash => cash.TransactionNo != null && cash.TransactionNo.StartsWith(prefix))
                   .OrderByDescending(cash => cash.TransactionNo.Substring(cash.TransactionNo.Length - 4))
                   .Select(cash => cash.TransactionNo)
                   .FirstOrDefault();
        }

        public void AddOrder(Order order)
        {
            _repository = new OrderRepository();
            _repository.Add(order);
        }

        public void UpdateOrder(Order order)
        {
            _repository = new OrderRepository();
            _repository.Update(order);
        }

        public void CompleteOrder(Order order)
        {
            _repository = new OrderRepository();
            order.Date = DateOnly.FromDateTime(DateTime.Now);
            order.Status = true;

            _repository.Update(order);
        }

        public Order? GetOrderByTransactionNo(string transactionNo)
        {
            _repository = new OrderRepository();
            List<Order> orders = _repository.GetAll();

            return orders
                   .FirstOrDefault(order => order.TransactionNo.Equals(transactionNo));
      
        }
    }
}

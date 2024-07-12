using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IOrderService
    {
        string? GetMaxTransactionNoStartingWithDate(string prefix);

        void AddOrder(Order order);

        void CompleteOrder(Order order);

        Order? GetOrderByTransactionNo(string transactionNo);
    }
}

using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Impl
{
    public class OrderServiceImpl : IOrderService
    {
        private CarWashContext _context;

        public string? GetMaxTransactionNoStartingWithDate(string prefix)
        {
            _context = new CarWashContext();

            return _context.Orders
                   .Where(cash => cash.TransactionNo != null && cash.TransactionNo.StartsWith(prefix))
                   .OrderByDescending(cash => cash.TransactionNo.Substring(cash.TransactionNo.Length - 4))
                   .Select(cash => cash.TransactionNo)
                   .FirstOrDefault();
        }
    }
}

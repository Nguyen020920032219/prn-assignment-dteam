using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Impl
{
    public class CashServiceImpl : ICashService
    {
        private CarWashContext _context;

        public string? GetMaxTransactionNoStartingWithDate(string prefix)
        {
            _context = new CarWashContext();

            return _context.TbCashes
                   .Where(cash => cash.Transno != null && cash.Transno.StartsWith(prefix))
                   .OrderByDescending(cash => cash.Transno.Substring(cash.Transno.Length - 4))
                   .Select(cash => cash.Transno)
                   .FirstOrDefault();
        }
    }
}

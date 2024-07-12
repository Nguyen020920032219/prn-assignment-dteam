using Repository.Entities;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
   public class AccountService
    {
        private AccountRepository _repository;

        public Account? GetEmployeeByUserNameAndPassword(string userName, string password)
        {
            _repository = new AccountRepository();

            return _repository.GetAll()
                              .FirstOrDefault(account => account.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase) &&
                                              account.Password.Equals(password, StringComparison.OrdinalIgnoreCase)
                                              );
        }
    }
}

using Repository;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Impl
{
    public class AccountService : IAccountService
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

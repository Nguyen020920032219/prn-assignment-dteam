using Repository;
using Repository.Entities;

namespace Service
{
    public class AccountService
    {
        AccountRepository _accountRepository;

        public AccountService()
        {
            _accountRepository = new AccountRepository();
        }

        public List<Account> GetAccounts()
        {
            return _accountRepository.GetAll();
        }

        public List<Account> getListAccount() {
            List<Account> accounts = _accountRepository.GetAll();   
            List<Account> result = new List<Account>();
            foreach (Account account in accounts)
            {
                if (account.IsActive == true)
                {
                    result.Add(account);
                }
            }
            return result;
        }

        public List<Account> GetAccountsByUsername(string username) { 
            List<Account> accounts = _accountRepository.GetAll();
            List<Account> result = new List<Account>();
            foreach (Account account in accounts) 
                {
                if (account.UserName.ToLower().Contains(username.ToLower()))
                {
                    result.Add(account);
                }
                }
            return result;
        }

        public void UpdateAccount(Account account)
        {
            _accountRepository.Update(account);
        }

        public void CreateAccount(Account account)
        {
            _accountRepository.Add(account);
        }
    }
}

using System.Collections.Generic;
using Whatsup_Her.Models;

namespace Whatsup_Her.Repositories
{
    public interface IAccountRepository
    {
        List<Account> GetAllAccounts();
        Account GetAccountById(int id);
        Account GetAccount(string mobileNumber, string password);
        void Add(Account account);
        void Change(Account account);
        void Remove(int id);
    }
}
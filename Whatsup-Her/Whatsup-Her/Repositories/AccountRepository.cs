using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Whatsup_Her.Models;

namespace Whatsup_Her.Repositories
{
    public class AccountRepository
    {
        private WhatsUpContext db = new WhatsUpContext();

        public void Add(Account account)
        {
            db.Accounts.Add(account);
            db.SaveChanges();
        }

        public void Change(Account account)
        {
            db.Entry(account).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }

        public Account GetAccount(string mobileNumber, string password)
        {
            try
            {
                Account account = db.Accounts.FirstOrDefault(a => a.MobileNumber == mobileNumber && a.Password == password);
                return account;
            }
            catch (InvalidOperationException e)
            {
                return null;
            }
        }

        public Account GetAccountById(int? id)
        {
            return db.Accounts.Find(id);
        }

        public List<Account> GetAllAccounts()
        {
            return db.Accounts.ToList();
        }

        public void Remove(int id)
        {
            Account account = GetAccountById(id);
            db.Accounts.Remove(account);
            db.SaveChanges();
        }

        public Account GetContactByMobile(string mobileNumber)
        {
            try
            {
                Account account = db.Accounts.Single(a => a.MobileNumber == mobileNumber);
                return account;
            }
            catch{
                return null;
            }
        }

        internal object GetContactByEmail(string emailAddress)
        {
            try
            {
                Account account = db.Accounts.Single(a => a.EmailAddress == emailAddress);
                return account;
            }
            catch
            {
                return null;
            }
        }
    }
}
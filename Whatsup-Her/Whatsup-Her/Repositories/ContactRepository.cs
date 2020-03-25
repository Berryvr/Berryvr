using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Whatsup_Her.Models;


namespace Whatsup_Her.Repositories
{
    public class ContactRepository
    {
        private WhatsUpContext db = new WhatsUpContext();
        private AccountRepository accountRepository = new AccountRepository();

        public IEnumerable<Contact> GetAllContacts(int OwnerAccId)
        {
            //IEnumerable<Contact> contacts = db.Contacts.Where(a => a.OwnerAccount.Id == OwnerAccId);
            IEnumerable<Contact> contacts = db.Contacts.Where(c => c.OwnerAccountId == OwnerAccId);
            return contacts;
        }

        public Contact GetContactById(int? id)
        {
            return db.Contacts.Find(id);
        }

        public Contact GetWithOwner(int owneracc, int contactacc)
        {
            Contact contact = db.Contacts.FirstOrDefault(m => m.ContactAccountId == contactacc && m.OwnerAccountId == owneracc);

            return contact;
        }

        public bool Add(Contact contact, Account account)
        {

            contact.OwnerAccountId = account.Id;
            //Check if contact already exists
            Account contactAccount = accountRepository.GetContactByMobile(contact.MobileNumber);
            if (contactAccount == null)
            {
                return false;
            }

            contact.ContactAccountId = contactAccount.Id;
            db.Contacts.Add(contact);
            db.SaveChanges();
            return true;
        }

        public void Change(Contact contact)
        {
            Contact temp = GetContactById(contact.Id);
            temp.Name = contact.Name;

            db.Entry(temp).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Remove(Contact cid)
        {
            Contact contact = db.Contacts.Find(cid.Id);
            db.Contacts.Remove(contact);
            db.SaveChanges();
        }
    }
}
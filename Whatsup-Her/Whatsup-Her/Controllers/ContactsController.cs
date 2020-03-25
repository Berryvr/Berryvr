using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Whatsup_Her.Models;
using Whatsup_Her.Repositories;

namespace Whatsup_Her.Controllers
{
    
    public class ContactsController : Controller
    {
        // private WhatsUpDb db = new WhatsUpDb();
        private ContactRepository repository = new ContactRepository();
        private AccountRepository accountRepository = new AccountRepository();

        // GET: Contacts
        public ActionResult Index()
        {
            if (Session["loggedIn_account"] != null)
            {
                Account a = (Account)Session["loggedIn_account"];
                return View(repository.GetAllContacts(a.Id));
            } else
            {
                return Redirect("~/");
            }
        }

        // GET: Contacts/Details/
        public ActionResult Details(int? id)
        {
            if (Session["loggedIn_account"] != null) {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Contact contact = repository.GetContactById(id);
                if (contact == null)
                {
                    return HttpNotFound();
                }
                return View(contact); 
            } else
            {
                return Redirect("~/");
            }
        }

        public ActionResult Chat(Contact contact)
        {
            return RedirectToAction("Message", "Chat", new { OtherAccountId = contact.ContactAccountId });
        }

        // GET: Contacts/Create
        public ActionResult Create()
        {
            if (Session["loggedIn_account"] != null)
            {
                return View();
            } else
            {
                return Redirect("~/");
            }
        }

        // POST: Contacts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ContactId,Name,MobileNumber")] Contact contact)
        {
            
                if (ModelState.IsValid)
                {
                    Account a = (Account)Session["loggedIn_account"];
                    contact.OwnerAccountId = a.Id;

                //test if existing Account
                    Account testContact = accountRepository.GetContactByMobile(contact.MobileNumber);
                    if (testContact != null)
                    {
                        repository.Add(contact, a);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "There is no account with this Mobile number";
                        return View();
                    }
                }

                return View(contact);

        }

        // GET: Contacts/Edit/
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = repository.GetContactById(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // POST: Contacts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ContactId,Name,MobileNumber")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                repository.Change(contact);
                return RedirectToAction("Index");
            }
            return View(contact);
        }

        // GET: Contacts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = repository.GetContactById(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // POST: Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Contact contact = repository.GetContactById(id);
            repository.Remove(contact);
            return RedirectToAction("Index");
        }
    }
}

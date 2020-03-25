using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Whatsup_Her.Models;
using Whatsup_Her.Repositories;

namespace Whatsup_Her.Controllers
{
    public class AccountController : Controller
    {
        AccountRepository repository = new AccountRepository();

        // GET: Accounts
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Contacts");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                // Get account with given credentials
                Account account = repository.GetAccount(model.MobileNumber, model.Password);
                if (account != null)
                {
                    FormsAuthentication.SetAuthCookie(account.MobileNumber, false);

 
                    Session["Loggedin_account"] = account;


                    return RedirectToAction("Index", "Contacts");
                }
                else
                {
                    ModelState.AddModelError("login-error", "The username or password is incorrect");
                }
            }
            return View();
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Accounts");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Account account)
        {
            if (ModelState.IsValid)
            {
                // Test if phone number has been registered
                if (repository.GetContactByMobile(account.MobileNumber) != null)
                {
                    ViewBag.Error = "this Mobilenumber is already in use";
                    return View("Error");
                }

                // Test if email has been registered
                if (repository.GetContactByEmail(account.EmailAddress) != null)
                {
                    ViewBag.Error = "This Mail address is already in use";
                    return View("Error");
                }

                repository.Add(account);

                ViewBag.successMessage = "You have been succesfully registered. You can log in with the credentials you entered before.";
                return View("Success");
            }
            else { return View(); }
        }
    }
}
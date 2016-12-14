using OnlineTrener.Context;
using OnlineTrener.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace OnlineTrener.Controllers
{
    public class AuthController : Controller
    {
        UsersContext db = new UsersContext();
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Login()
        {
            return View(new AuthLogin { });
        }
        [HttpPost]
        public ActionResult Login(AuthLogin form, string returnUrl)
        {
            var user = db.Users.FirstOrDefault(u => u.username == form.Username);
            if (user == null || !user.CheckPassword(form.Password))
                ModelState.AddModelError("Username", "Username or password are incorrect!");

            if (!ModelState.IsValid)
            return View(form);
                
            FormsAuthentication.SetAuthCookie(user.username, true);

            if (!string.IsNullOrWhiteSpace(returnUrl))
            return RedirectToAction(returnUrl);
            
            return RedirectToAction("Index", "Home");
            
       }
    }
}
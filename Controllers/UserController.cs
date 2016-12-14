using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineTrener.ViewModels;
using OnlineTrener.Context;
using OnlineTrener.Models;
using Microsoft.AspNet.Identity;
using System.Web.Security;
using System.Net;
using System.Data.Entity;

namespace OnlineTrener.Controllers
{
    public class UserController : Controller
    {
        UsersContext db = new UsersContext();
        public int PageSize = 6;

        // GET: User
        public ActionResult Index(int page = 1)
        {
            UsersList model = new UsersList
            {
                Users = db.Users
                .OrderBy(u => u.userId)
                .Skip((page-1)*PageSize)
                .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = db.Users.Count()
                }
            };


            return View(model);
        }

       
        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(UserNew form)
        {
            if (db.Users.Any(u => u.username == form.username))
            {
                ModelState.AddModelError("Username", "Username must be unique");
            }

            if (ModelState.IsValid)
            {
                var user = new User
                {
                    username = form.username,
                    email = form.email
                };
                user.SetPassword(form.password);
                //MembershipCreateStatus createStatus;
                //Membership.CreateUser(form.username, form.password, form.email, null, null, true, null, out createStatus);

                db.Users.Add(user);
                db.SaveChanges();

                return RedirectToAction("Index");
            }


            return View(form);
        }

        // GET: User/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(new UserEdit {username = user.username, email=user.email });
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, UserEdit form)
        {
            var user = db.Users.Find(id);

            if (db.Users.Any(u => u.username == form.username && u.userId !=id))
                {
                ModelState.AddModelError("Username", "Username must be unique");
                }
            
            if (ModelState.IsValid)
            {
                
                user.username = form.username;
                user.email = form.email;
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(form);
        }

        
        public ActionResult ResetPassword (int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(new UsersResetPassword { username = user.username });
        }

        [HttpPost]
        public ActionResult ResetPassword(int id, UsersResetPassword form)
        {
            if (ModelState.IsValid)
            {
                User user = db.Users.Find(id);
                user.SetPassword(form.Password);
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(form);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {

            User user = db.Users.Find(id);


            if (user == null)
                return HttpNotFound();
            else
                return View(user);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Delete (int id, string confirmButton)
        {
            User user = db.Users.Find(id);
            if(user == null)
            {
                return HttpNotFound();
            }
            else
            {
                db.Users.Remove(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}

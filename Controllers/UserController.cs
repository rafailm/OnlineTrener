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
using Microsoft.AspNet.Identity.EntityFramework;

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
            RolesContext rdb = new RolesContext();
            return View(new UserNew {
                Roles = rdb.Roles.Select(role => new RoleCheckbox
                {
                    roleId = role.roleId,
                    IsChecked = false,
                    roleName = role.roleName
                }).ToList()
            });
        }

        // POST: User/Create
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create(UserNew form)
        {
            var user = new User();
            
            SyncRoles(form.Roles, user.Roles);
           

            if (db.Users.Any(u => u.username == form.username))
            {
                ModelState.AddModelError("Username", "Username must be unique");
            }

            if (ModelState.IsValid)
            {
                user.username = form.username;
                user.email = form.email;                 
                user.SetPassword(form.password);
                
                db.Users.Add(user);
                db.SaveChanges();

                return RedirectToAction("Index");
            }


            return View(form);
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            User user = db.Users.Find(id);
            RolesContext rdb = new RolesContext();
            
            if (user == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           
            return View(new UserEdit {username = user.username, email = user.email,
                Roles = rdb.Roles.Select(role => new RoleCheckbox
                {
                    roleId = role.roleId,
                    IsChecked = user.Roles.Contains(role),
                    roleName = role.roleName
                }).ToList()
            });
        }

        // POST: User/Edit/5
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(int id, UserEdit form)
        {

            var user = db.Users.Find(id);
            SyncRoles(form.Roles, user.Roles);

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

        [HttpPost, ValidateAntiForgeryToken]
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

        [AcceptVerbs(HttpVerbs.Post), ValidateAntiForgeryToken]
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
        private void SyncRoles(IList<RoleCheckbox> checkboxes, IList<Role> roles)
        {
            var selectedRoles = new List<Role>();
            RolesContext rdb = new RolesContext();

           
            List <Role> roleList = new List<Role>();
            roleList = rdb.Roles.ToList();
            

           
            foreach (var role in roleList)
            {
                var checkbox = checkboxes.Single(c => c.roleId == role.roleId);
                checkbox.roleName = role.roleName;

                if (checkbox.IsChecked)
                    selectedRoles.Add(role);
            }
            foreach (var toAdd in selectedRoles.Where(t => !roles.Contains(t)))
            {
                roles.Add(toAdd);
            }
            foreach (var toRemove in roles.Where(t => !selectedRoles.Contains(t)).ToList())
            {
                roles.Remove(toRemove);
            }
        }
    }
}

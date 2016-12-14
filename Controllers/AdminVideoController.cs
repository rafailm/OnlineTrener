using OnlineTrener.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineTrener.ViewModels;
using System.Data.Entity;
using System.Net;

namespace OnlineTrener.Controllers
{
    [Authorize(Roles ="admin")]
    public class AdminVideoController : Controller
    {
        VideoContext db = new VideoContext();
        
        // GET: AdminVideo
        public ActionResult Index()
        {
            VideoList vl = new VideoList { Videos = db.Videos.ToList() };
            return View(vl);
        }

       
        // GET: AdminVideo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminVideo/Create
        [HttpPost]
        public ActionResult Create(Video video)
        {
            video.IsAprooved = true;
            if (ModelState.IsValid)
            {
                db.Videos.Add(video);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
                return View(video);
        }

        // GET: AdminVideo/Edit/5
        public ActionResult Edit(int Id)
        {
            Video vl = db.Videos.FirstOrDefault(v => v.videoId == Id);
            return View(vl);
        }

        // POST: AdminVideo/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "videoId, videoTitle, videoDescription, IsAprooved, videoUrl, videoCategory")] Video video)
        {
            if (ModelState.IsValid)
            {
                db.Entry(video).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(video);
        }

        // GET: AdminVideo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Video video = db.Videos.Find(id);
            if (video == null)
            {
                return HttpNotFound();
            }
            return View(video);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Video video = db.Videos.Find(id);
            db.Videos.Remove(video);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UnAprooved()
        {
            VideoList vl = new VideoList
            {
                Videos = db.Videos
                .Where(v => v.IsAprooved == false)

        };
            
            return View(vl);
        }

        // GET: AdminVideo/Edit/5
        public ActionResult EditUnaprooved(int Id)
        {
            Video vl = db.Videos.FirstOrDefault(v => v.videoId == Id);
            return View(vl);
        }

        // POST: AdminVideo/Edit/5
        [HttpPost]
        public ActionResult EditUnaprooved([Bind(Include = "videoId, videoTitle, videoDescription, IsAprooved, videoUrl, videoCategory")] Video video)
        {
            if (ModelState.IsValid)
            {
                db.Entry(video).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("UnAprooved");
            }
            return View(video);
        }

        // GET: AdminVideo/Delete/5
        public ActionResult DeleteUnAprooved(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Video video = db.Videos.Find(id);
            if (video == null)
            {
                return HttpNotFound();
            }
            return View(video);
        }

        [HttpPost, ActionName("DeleteUnAprooved")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteUnAproovedConfirmed(int id)
        {
            Video video = db.Videos.Find(id);
            db.Videos.Remove(video);
            db.SaveChanges();
            return RedirectToAction("UnAprooved");
        }
    }


}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineTrener.ViewModels;
using OnlineTrener.Models;
using System.Text;

namespace OnlineTrener.Controllers
{
    
    public class VideoController : Controller
    {

        public string YoutubeLink (string url= "www.youtube.com/watch?v=3dHQc1tJA6o")
        {
            string a = "https://www.youtube.com/";
            char[] elevenChar = new char[24];
            int count=0;
            int j = 0;
            string test = new string(elevenChar);
            for(int i=0;i<24;i++)
            {
                if(url[j] == a[i])
                {
                    
                        elevenChar[count] = a[i];
                        count++;
                        j++;
                    if(test == "www.youtube.com/")
                    {
                        break;
                    }
                }
                else
                {
                    j = 0;
                }
            }
            string result = new string(elevenChar);
            return result;
        }
        VideoContext db = new VideoContext();
        public int PageSize = 6;

        // GET: Video
        //public ActionResult Index()
        //{
        //  //VideoList vl = new VideoList { Videos = db.Videos.ToList() };

                       
        //    return View(new VideoList { Videos = db.Videos.ToList() });
        //}

         public ActionResult Index(string category, int page = 1)
        {
            YoutubeLink();
            VideoList model = new VideoList {
                 Videos = db.Videos
                .Where(v => category == null || v.videoCategory == category)
                .Where(v=> v.IsAprooved == true)
                .OrderBy(v => v.videoId)
                .Skip((page - 1) * PageSize)
                .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null ?
                    db.Videos.Count() :
                    db.Videos.Where(v => v.videoCategory == category).Count()
                },
                CurrentCategory = category
            };
            return View(model);


            //return View(db.Videos
            //    .OrderBy(v=> v.videoId)
            //    .Skip((page-1)*PageSize)
            //    .Take(PageSize));
        }

        // GET: Video/Details/5
        [ValidateAntiForgeryToken, Authorize(Roles = "admin")]
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Video/Create
        [ValidateAntiForgeryToken,Authorize(Roles ="admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Video/Create
        [HttpPost]
        [ValidateAntiForgeryToken, Authorize(Roles = "admin")]
        public ActionResult Create(Video video)
        {
            video.IsAprooved = false; 

            if (ModelState.IsValid)
                {
                    db.Videos.Add(video);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }


            return View(video);
        }

        // GET: Video/Edit/5
        [ValidateAntiForgeryToken, Authorize(Roles = "admin")]
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Video/Edit/5
        [ValidateAntiForgeryToken, Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Video/Delete/5
        [ValidateAntiForgeryToken, Authorize(Roles = "admin")]
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Video/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken, Authorize(Roles = "admin")]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

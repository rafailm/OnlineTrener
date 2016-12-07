using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineTrener.ViewModels;

namespace OnlineTrener.Controllers
{
    public class NavController : Controller
    {
        VideoModel db = new VideoModel();

        public PartialViewResult Menu(string category = null)
        {
            ViewBag.SelectedCategory = category;
            IEnumerable<string> categories = db.Videos
            .Select(x => x.videoCategory)
            .Distinct()
            .OrderBy(x => x);
            return PartialView(categories);
        } 
     }
}
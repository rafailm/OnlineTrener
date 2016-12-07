using OnlineTrener.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineTrener.ViewModels
{
    public class VideoList
    {
        public IEnumerable<Video> Videos { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentCategory { get; set; }
    }
}
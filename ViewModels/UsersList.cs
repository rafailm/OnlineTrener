using Microsoft.AspNet.Identity;
using OnlineTrener.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineTrener.ViewModels
{
    public class UsersList
    {
        public IEnumerable<User> Users {get; set;}
        public PagingInfo PagingInfo { get; set; }
    }

   
}
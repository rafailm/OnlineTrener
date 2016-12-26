using OnlineTrener.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OnlineTrener.Context;

namespace OnlineTrener
{
    public class Auth
    {
        private const string UserKey = "OnlineTrener.Auth.UserKey";
        public static User User
        {
            get
            {
                if (!HttpContext.Current.User.Identity.IsAuthenticated)
                    return null;

                var user = HttpContext.Current.Items[UserKey] as User;

                if(user==null)
                {
                    UsersContext db = new UsersContext();
                    user = db.Users.FirstOrDefault(u => u.username == HttpContext.Current.User.Identity.Name);

                    if (user == null)
                        return null;
                    HttpContext.Current.Items[UserKey] = user;
                }
                return user;
            }
        }
    }
}
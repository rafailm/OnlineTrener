using OnlineTrener.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineTrener.Models
{
    public class User
    {
        [HiddenInput(DisplayValue = false)]
        public int userId { get; set; }

        [Required]
        [StringLength(128)]
        public string username { get; set; }

        [Required, DataType(DataType.EmailAddress)]
        [StringLength(256)]
        public string email { get; set; }

        [Required]
        [StringLength(128)]
        public string password_hash { get; set; }

        public virtual IList<Role> Roles { get; set; }

        public User()
        {
            Roles = new List<Role>();
        }

        public void SetPassword(string password)
        {
            password_hash = BCrypt.Net.BCrypt.HashPassword(password, 14);
        }
        public virtual bool CheckPassword(string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, password_hash);
        }

        
    }
}
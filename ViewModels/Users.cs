namespace OnlineTrener.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    public class RoleCheckbox
    {
        public int roleId { get; set; }
        public bool IsChecked { get; set; }
        public string roleName { get; set; }

    }
    public class UserNew
    {
        public IList<RoleCheckbox> Roles { get; set; }

        [Key, HiddenInput(DisplayValue = false)]
        public int userId { get; set; }

        [Required]
        [StringLength(128)]
        public string username { get; set; }

        [Required, DataType(DataType.EmailAddress)]
        [StringLength(256)]
        public string email { get; set; }

        [Required, DataType(DataType.Password)]
        [StringLength(128)]
        public string password { get; set; }
    }
    public class UserEdit
    {
        public IList<RoleCheckbox> Roles { get; set; }

        [Required]
        [StringLength(128)]
        public string username { get; set; }

        [Required, DataType(DataType.EmailAddress)]
        [StringLength(256)]
        public string email { get; set; }
    }
    public class UsersResetPassword
    {
        public string username { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
    } 

   public class UsersAll
    {
        public IEnumerable<User> Users { get; set; }
    }


}


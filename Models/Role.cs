namespace OnlineTrener.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    public partial class Role
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int roleId { get; set; }

        [Required]
        [StringLength(128)]
        public string roleName { get; set; }

    }
}

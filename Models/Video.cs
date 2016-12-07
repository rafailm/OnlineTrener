    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

namespace OnlineTrener.Models
{
    public class Video
    { 
        [HiddenInput(DisplayValue = false)]
        public int videoId { get; set; }


        [Required(ErrorMessage = "Pisi naslov, jebo ti pas mater")]
        [StringLength(50)]
        public string videoTitle { get; set; }


        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Please enter a description")]
        [StringLength(1000)]
        public string videoDescription { get; set; }


        [Required(ErrorMessage = "Please enter a category")]
        [StringLength(50)]
        public string videoCategory { get; set; }


        [Required(ErrorMessage = "A GDE JE VIDEO?")]
        [StringLength(400)]
        public string videoURL { get; set; }

        public bool? IsAprooved { get; set; }
    }

}
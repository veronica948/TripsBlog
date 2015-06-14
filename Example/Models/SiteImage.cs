using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class SiteImage
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string ImageSrc { get; set; }
    }
}
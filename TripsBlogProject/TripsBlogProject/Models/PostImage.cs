using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TripsBlogProject.Models
{
    public class PostImage
    {
        [Required]
        public int PostImageId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string ImageSrc { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TripsBlogProject.Models
{
    public class CountryImage
    {
        [Required]
        public int CountryImageId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string ImageSrc { get; set; }
 
    }
}
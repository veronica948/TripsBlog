using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TripsBlogProject.Models
{
    public class Country
    {
        [Required]
        public int CountryId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        
        public CountryImage  ImageUrl {get; set;}
    }
}
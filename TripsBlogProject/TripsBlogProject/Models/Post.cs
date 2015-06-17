using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TripsBlogProject.Models
{
    public class Post
    {
        [Required]
        public int PostId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Title { get; set; }

        [Required]
        public Country Country { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Place { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required]
        public ApplicationUser Author { get; set; }
        
        [DataType(DataType.ImageUrl)]
        public string  Image {get; set;}
    }
    public class CreatePostModel
    {
        [Required]
        public int PostId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Title { get; set; }

        [Required]
        public CountriesListViewModel AllCountries { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Place { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

    }
}
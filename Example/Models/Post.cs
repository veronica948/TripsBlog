using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Post
    {
        [Required]
        public int PostId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required]
        public ApplicationUser Author { get; set; }
        

        public List<Tag> Tags { get; set; }

        public SiteImage  ImageUrl {get; set;}
    }

    public class CreatePostViewModel
    {
        [Required]
        public int PostId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
    }
    public class EditPostViewModel
    {
        [Required]
        public int PostId { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string Title { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public List<Tag> AvailableTags { get; set; }
    }
}
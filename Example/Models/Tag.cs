using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Tag
    {
        [Required]
        public int TagId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string TagName { get; set; }
    }
}
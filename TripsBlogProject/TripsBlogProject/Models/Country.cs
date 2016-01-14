using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TripsBlogProject.Models
{
    public class Country
    {
        [Required]
        public int CountryId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        
        [DataType(DataType.ImageUrl)]
        public string  ImageUrl {get; set;}
    }
    public class CountriesListViewModel
    {
        // Display Attribute will appear in the Html.LabelFor
        [Display(Name = "Country")]
        public int SelectedCountryId { get; set; }
        public IEnumerable<SelectListItem> Countries { get; set; }
    }
}
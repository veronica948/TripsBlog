using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace CountryService.Models
{
    [DataContract(Namespace="")]
    public class Country
    {
        [Required]
        [DataMember]
        public int CountryId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [DataMember]
        public string Name { get; set; }


        [DataType(DataType.MultilineText)]
        [DataMember]
        public string Description { get; set; }
    }
}
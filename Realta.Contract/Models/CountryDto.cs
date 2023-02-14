using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Contract.Models
{
    public class CountryDto
    {
        [Required(ErrorMessage = "country_id is required")]
        public int country_id { get; set; }
        [Required]
        [MinLength(5, ErrorMessage = "country_name must larger than 5")]
        [MaxLength(55, ErrorMessage = "country_name cannot be longer than 55")]
        public string? country_name { get; set; }
        public int country_region_id { get; set; }
    }
}

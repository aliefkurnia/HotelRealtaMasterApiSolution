using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Contract.Models
{

    public class RegionsDto
    {
        [Required(ErrorMessage = "region_code is required")]
        public int regionCode { get; set; }
        [Required]
        [MinLength(5, ErrorMessage = "region_name must larger than 5")]
        [MaxLength(35, ErrorMessage = "region_name cannot be longer than 35")]
        public string? RegionName { get; set; }
    }
    
}

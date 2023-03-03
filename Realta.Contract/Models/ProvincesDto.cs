using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Contract.Models
{
    public class ProvincesDto
    {
        [Required(ErrorMessage = "prov_id is required")]
        public int ProvId { get; set; }
        [Required]
        [MinLength(5, ErrorMessage = "prov_name must larger than 5")]
        [MaxLength(85, ErrorMessage = "prov_name cannot be longer than 85")]
        public string? ProvName { get; set; }
        public int ProvCountryId { get; set; }
    }
}

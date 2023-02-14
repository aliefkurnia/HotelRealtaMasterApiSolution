using Microsoft.SqlServer.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Contract.Models
{
    public class AddressDto
    {
        [Required(ErrorMessage ="addr_id is required")]
        public int addr_id { get; set; }
        
        [Required]
        [MinLength(10, ErrorMessage ="address line 1 must larger than 10")]
        public string addr_line1 { get; set; }

        public string addr_line2 { get; set; }

        [MaxLength(5,ErrorMessage ="postal code cannot be longer than 5")]
        public string addr_postal_code { get; set; }
        public string addr_spatial_location { get; set; }
        public int addr_prov_id { get; set; }
    
    }
}

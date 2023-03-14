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
        public int AddrId { get; set; }
        
        [Required]
        [MinLength(10, ErrorMessage ="address line 1 must larger than 10")]
        public string AddrLine1 { get; set; }

        public string AddrLine2 { get; set; }
        public  string AddrCity { get; set; }

        [MaxLength(5,ErrorMessage ="postal code cannot be longer than 5")]
        public string AddrPostalCode { get; set; }
        public string AddrSpatialLocation { get; set; }
        public int AddrProvId { get; set; }
    
    }
}

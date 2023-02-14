using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Contract.Models
{
    public class MembersDto
    {
        [Key]
        [MaxLength(15, ErrorMessage = "memb_name must between 1 and 15")]
        public string memb_name { get; set; }

        public string memb_description { get; set; }
    }
}

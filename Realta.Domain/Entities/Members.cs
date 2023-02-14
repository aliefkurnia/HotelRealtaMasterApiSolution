using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Domain.Entities
{
    [Table("Members")]
    public class Members
    {
        [Key]
        [MaxLength(15, ErrorMessage = "memb_name must between 1 and 15")]
        public string memb_name { get; set; }

        public string memb_description { get; set; }
    }
}

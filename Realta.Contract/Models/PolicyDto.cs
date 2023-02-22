using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Contract.Models
{
    public class PolicyDto
    {
        [Key] public int poli_id { get; set; }
        [Required] public string poli_name { get; set; }
        public string poli_description { get; set; }

    }
}

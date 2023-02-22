using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Domain.Entities
{
    [Table("Policy")]
    public class Policy
    {
        [Key] public int poli_id { get; set; }
        [Required] public string poli_name { get;set; }
        public string poli_description { get; set; }

    }
}

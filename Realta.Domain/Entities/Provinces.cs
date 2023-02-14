using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Domain.Entities
{
    [Table("Provinces")]
    public class Provinces
    {
        [Key]
        public int prov_id { get; set; }
        [Required]
        public string prov_name { get; set; }
        public int prov_country_id { get; set; }
    }
}

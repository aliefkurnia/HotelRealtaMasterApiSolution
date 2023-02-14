using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Domain.Entities
{
    [Table("Country")]
    public class Country
    {
        [Key]
        public int country_id { get; set; }
        [Required]
        public string country_name { get; set; }
        [ForeignKey("country_region_id")]
        public int country_region_id { get; set; }
    }
}

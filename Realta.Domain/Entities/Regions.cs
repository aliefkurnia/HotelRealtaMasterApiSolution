using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Domain.Entities
{
    [Table("Regions")]
    public class Regions
    {
        [Key]
        public int RegionCode { get; set; }
        [Required]
        public string? RegionName { get; set; }
    }
}

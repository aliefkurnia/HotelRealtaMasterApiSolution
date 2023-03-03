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
        public int ProvId { get; set; }
        [Required]
        public string ProvName { get; set; }
        public int ProvCountryId { get; set; }
    }
}

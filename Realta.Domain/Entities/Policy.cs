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
        [Key] public int PoliId { get; set; }
        [Required] public string PoliName { get;set; }
        public string PoliDescription { get; set; }

    }
}

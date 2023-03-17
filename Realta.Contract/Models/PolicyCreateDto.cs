using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Contract.Models
{
    public class PolicyCreateDto
    {
        [Required] public string PoliName { get; set; }
        public string PoliDescription { get; set; }

    }
}

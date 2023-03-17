using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Contract.Models
{
    public class ServiceTaskCreateDto
    {
        [Required]
        public string SetaName { get; set; }
        public short SetaSeq { get; set; }

    }
}

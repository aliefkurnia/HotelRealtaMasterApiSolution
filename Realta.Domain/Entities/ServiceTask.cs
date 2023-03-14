using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Domain.Entities
{
    public class ServiceTask
    {
        [Key]
        public int SetaId { get; set; }
        [Required]
        public string SetaName { get;set; }
        public short SetaSeq { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Domain.Entities
{
    public class Service_Task
    {
        [Key]
        public int seta_id { get; set; }
        [Required]
        public string seta_name { get;set; }
        public short seta_seq { get; set; }

    }
}

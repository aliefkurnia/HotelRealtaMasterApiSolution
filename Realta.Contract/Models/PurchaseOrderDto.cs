using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Contract.Models
{
    public class StatusUpdateDto
    {
        [Required(ErrorMessage = "Status is required")]
        [Range(1, 5, ErrorMessage = "Status must be between 1 and 5.")]
        public byte PoheStatus { get; set; }
    }

    public class QtyUpdateDto
    {
        public short PodeOrderQty { get; set; }
        public decimal PodeReceivedQty { get; set; } = 0;
        public decimal PodeRejectedQty { get; set; } = 0;
    }
}

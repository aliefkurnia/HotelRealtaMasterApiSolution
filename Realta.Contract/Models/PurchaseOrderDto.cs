using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Contract.Models
{
    public class PurchaseOrderDto
    {
        public int PoVendorId { get; set; }
        public int PoEmpId { get; set; }
        public string PoPayType { get; set; }
        public int PoStockId { get; set; }
        public short PoOrderQty { get; set; }
        public decimal PoPrice { get; set; }
    }

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

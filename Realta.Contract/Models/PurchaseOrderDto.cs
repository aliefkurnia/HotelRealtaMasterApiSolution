using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Contract.Models
{
    public class PurchaseOrderDto
    {
        public int PoheId { get; set; }
        public string VendorName { get; set; }
        public string PoheNumber { get; set; }
        public byte PoheStatus { get; set; } = 1;
        public DateTime PoheOrderDate { get; set; } = DateTime.Now;
        public decimal PoheSubtotal { get; set; }
        public decimal PoheTax { get; set; } = 0.1m;
        public decimal PoheTotalAmount => PoheSubtotal + (PoheTax * PoheSubtotal);
        public decimal PoheRefund { get; set; } = 0;
        public DateTime? PoheArrivalDate { get; set; }
        public string PohePayType { get; set; }
        public int? PoheEmpId { get; set; }
        public int? PoheVendorId { get; set; }
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

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Contract.Models
{
    public class PurchaseOrderHeaderDto
    {
        public int PoheId { get; set; }
        public string PoheNumber { get; set; }
        public byte PoheStatus { get; set; } = 1;
        public DateTime PoheOrderDate { get; set; }
        public decimal? PoheSubtotal { get; set; }
        public decimal PoheTax { get; set; }
        public decimal? PoheTotalAmount { get; set; }
        public decimal PoheRefund { get; set; }
        public DateTime PoheArrivalDate { get; set; }
        public string PohePayType { get; set; }
        public int PoheEmpId { get; set; }
        public int PoheVendorId { get; set; }
    }

    public class StatusUpdateDto
    {
        [Required(ErrorMessage = "Status is required")]
        [Range(1,5, ErrorMessage = "Status must be between 1 and 5.")]
        public byte PoheStatus { get; set; }
    }

}

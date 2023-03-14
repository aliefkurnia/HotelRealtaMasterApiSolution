﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Realta.Domain.Entities
{
    [Table("Purchasing.PurchaseOrderHeader")]
    public class PurchaseOrderHeader
    {
        [Key]
        public int PoheId { get; set; }
        public string PoheNumber { get; set; }        
        public int LineItem { get; set; }

        public byte PoheStatus { get; set; } = 1;
        public DateTime PoheOrderDate { get; set; }
        public decimal PoheSubtotal { get; set; }
        public decimal PoheTax { get; set; }
        public decimal PoheTotalAmount { get; set; }
        public decimal PoheRefund { get; set; }
        public DateTime? PoheArrivalDate { get; set; }
        public string PohePayType { get; set; }
        public string VendorName { get; set; }

        [ForeignKey("Hr.Employee")]
        public int PoheEmpId { get; set; }


        [ForeignKey("Purchasing.Vendor")]
        public int PoheVendorId { get; set; }
    }
}

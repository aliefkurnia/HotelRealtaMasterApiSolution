namespace Realta.Contract.Models
{
    public class PurchaseOrderHeaderDto
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
        public int? PoheEmployeeId { get; set; }
        public int? PoheVendorId { get; set; }
    }
}

namespace Realta.Domain.Entities
{
    public class PurchaseOrderNested
    {
        public string PoheNumber { get; set; }
        public string VendorName { get; set; }
        public byte PoheStatus { get; set; }
        public DateTime PoheOrderDate { get; set; }
        public decimal PoheSubtotal { get; set; }
        public decimal PoheTotalAmount { get; set; }
        public IEnumerable<PurchaseOrderDetail>? Details { get; set; }
    }
}

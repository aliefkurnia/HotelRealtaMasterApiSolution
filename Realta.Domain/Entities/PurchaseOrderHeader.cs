using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Realta.Domain.Entities
{
    [Table("PurchaseOrderHeader")]
    public class PurchaseOrderHeader
    {
        [Key]
        public int pohe_id { get; set; }
        public string pohe_number { get; set; }
        public byte pohe_status { get; set; } = 1;
        public DateTime pohe_order_date { get; set; }
        public decimal? pohe_subtotal { get; set; }
        public decimal pohe_tax { get; set; }
        public decimal? pohe_total_amount { get; set; }
        public decimal pohe_refund { get; set; }
        public DateTime pohe_arrival_date{ get; set; }
        public string pohe_pay_type { get; set; }
        public int pohe_emp_id { get; set; }
        public int pohe_vendor_id{ get; set; }
    }
}

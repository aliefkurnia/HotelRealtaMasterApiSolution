using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Realta.Domain.Entities
{
    [Table("PurchaseOrderDetail")]
    public class PurchaseOrderDetail
    {
        [Key]
        public int pode_id { get; set; }
        public int pode_pohe_id { get; set; }
        public short pode_order_qty { get; set; }
        public decimal pode_price { get; set; }
        public decimal line_total { get; set; }
        public decimal pode_received_qty { get; set; }
        public decimal pode_rejected_qty { get; set; }
        public decimal pode_stocked_qty { get; set; }
        public DateTime pode_modified_date { get; set; }
        public int pode_stock_id { get; set; }

        [ForeignKey("pode_pohe_id")]
        public PurchaseOrderHeader PurchaseOrderHeader { get; set; }
    }
}

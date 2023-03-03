using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Domain.Entities
{
    [Table("Purchasing.stock_detail")]
    public class StockDetail
    {
        [Key]
        [Column(Order = 1)]
        public int? StodId { get; set; }
        [Key]
        [Column(Order = 2)]
        [ForeignKey("Purchasing.stocks")]
        public int? StodStockId { get; set; }
        public string? StodBarcodeNumber { get; set; }
        public string? StodStatus { get; set; }
        public string? StodNotes { get; set; }

        [ForeignKey("Hotel.facilities")]
        public int? StodFaciId { get; set; }

        [ForeignKey("Purchasing.purchase_order_header")]
        public int? StodPoheId { get; set; }

    }
}

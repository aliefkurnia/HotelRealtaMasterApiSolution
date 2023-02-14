using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Domain.Entities
{
    [Table("purchasing.stock_detail")]
    public class StockDetail
    {
        [Key]
        [Column(Order = 1)]
        public int? stod_id { get; set; }
        [Key]
        [Column(Order = 2)]
        [ForeignKey("purchasing.stocks")]
        public int? stod_stock_id { get; set; }
        public string? stod_barcode_number { get; set; }
        public string? stod_status { get; set; }
        public string? stod_notes { get; set; }

        [ForeignKey("Hotel.facilities")]
        public int? stod_faci_id { get; set; }

        [ForeignKey("purchasing.purchase_order_header")]
        public int? stod_pohe_id { get; set; }

    }
}

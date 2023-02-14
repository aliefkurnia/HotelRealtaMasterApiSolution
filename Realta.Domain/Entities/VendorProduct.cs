using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Domain.Entities
{
    [Table("Vendor_Product")]
    public class VendorProduct
    {
        [Key]
        public int vepro_id { get; set; }
        public int vepro_qty_stocked { get; set; }
        public int vepro_qty_remain { get; set; }
        public decimal vepro_price { get; set; }

        [ForeignKey("stock_id")]
        public int venpro_stock_id { get; set; }

        [ForeignKey("vendor_entity_id")]
        public int vepro_vendor_id { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Contract.Model
{
    public class VendorProductDto
    {
        public int vepro_id { get; set; }
        public int vepro_qty_stocked { get; set; }
        public int vepro_qty_remaining { get; set; }
        public decimal vepro_price { get; set; }
        public int venpro_stock_id { get; set; }
        public int vepro_vendor_id { get; set; }
    }
}

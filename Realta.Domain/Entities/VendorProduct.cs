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
        public int VeproId { get; set; }
        public string StockName { get; set; }
        public int VeproQtyStocked { get; set; }
        public int VeproQtyRemaining { get; set; }
        public decimal VeproPrice { get; set; }

        [ForeignKey("stock_id")]
        public int VenproStockId { get; set; }

        [ForeignKey("vendor_entity_id")]
        public int VeproVendorId { get; set; }

    }
}

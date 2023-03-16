using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Contract.Models
{
    public class VendorProductDto
    {
        public int VeproId { get; set; }
        public string? StockName { get; set; }
        public int VeproQtyStocked { get; set; }
        public int VeproQtyRemaining { get; set; }
        public decimal VeproPrice { get; set; }
        public int VenproStockId { get; set; }
        public int VeproVendorId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Contract.Models
{
    public class PurchaseOrderDto
    {
        public int PoVendorId { get; set; }
        public int PoEmpId { get; set; }
        public string PoPayType { get; set; }
        public int PoStockId { get; set; }
        public short PoOrderQty { get; set; }
        public decimal PoPrice { get; set; }
    }
}

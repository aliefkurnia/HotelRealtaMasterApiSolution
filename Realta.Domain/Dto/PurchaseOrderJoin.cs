using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Domain.Entities
{
    public class PurchaseOrderJoin
    {
        public string PoheNumber { get; set; }
        public string VendorName { get; set; }
        public byte PoheStatus { get; set; }
        public DateTime PoheOrderDate { get; set; }
        public decimal PoheSubtotal { get; set; }
        public decimal PoheTotalAmount { get; set; }

        public int PodeId { get; set; }
        public int PodePoheId { get; set; }
        public int PodeStockId { get; set; }
        public string StockName { get; set; }
        public short PodeOrderQty { get; set; }
        public decimal PodePrice { get; set; }
        public decimal PodeLineTotal { get; set; }
        public decimal PodeReceivedQty { get; set; }
        public decimal PodeRejectedQty { get; set; }
        public decimal PodeStockedQty { get; set; }
        public DateTime PodeModifiedDate { get; set; }
    }
}

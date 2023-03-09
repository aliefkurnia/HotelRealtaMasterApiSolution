using System;

namespace Realta.Contract.Models
{
    public class PurchaseOrderDetailDto
    {
        public int PodeId { get; set; }
        public string? StockName { get; set; }
        public int PodePoheId { get; set; }
        public short PodeOrderQty { get; set; }
        public decimal PodePrice { get; set; }
        public decimal PodeLineTotal { get; set; }
        public decimal PodeReceivedQty { get; set; } = 0;
        public decimal PodeRejectedQty { get; set; } = 0;
        public decimal PodeStockedQty { get; set; } = 0;
        public DateTime PodeModifiedDate { get; set; }
        public int PodeStockId { get; set; }
    }

    public class QtyUpdateDto
    {
        public short PodeOrderQty { get; set; }
        public decimal PodeReceivedQty { get; set; } = 0;
        public decimal PodeRejectedQty { get; set; } = 0;
    }
}

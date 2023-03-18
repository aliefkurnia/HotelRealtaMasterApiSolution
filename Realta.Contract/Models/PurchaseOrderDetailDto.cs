namespace Realta.Contract.Models
{
    public class PurchaseOrderDetailDto
    {
        public int PodeId { get; set; }
        public int PodePoheId { get; set; }
        public int PodeStockId { get; set; }
        public string StockName { get; set; }
        public short PodeOrderQty { get; set; }
        public decimal PodePrice { get; set; }
        public decimal PodeLineTotal { get; set; }
        public decimal PodeReceivedQty { get; set; }
        public decimal PodeRejectedQty { get; set; }
        public decimal PodeStockedQuantity => PodeReceivedQty - PodeRejectedQty;
        public DateTime PodeModifiedDate { get; set; }
    }
}

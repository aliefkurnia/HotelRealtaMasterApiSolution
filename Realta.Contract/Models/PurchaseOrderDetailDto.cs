namespace Realta.Contract.Models
{
    public class PurchaseOrderDetailDto
    {
        public int PodeId { get; set; }
        public int PodePoheId { get; set; }
        public int PodeStockId { get; set; }
        public string StockName { get; set; }
        public short PodeOrderQuantity { get; set; }
        public decimal PodePrice { get; set; }
        public decimal LineTotal { get; set; }
        public decimal PodeReceivedQuantity { get; set; }
        public decimal PodeRejectedQuantity { get; set; }
        public decimal PodeStockedQuantity => PodeReceivedQuantity - PodeRejectedQuantity;
        public DateTime PodeModifiedDate { get; set; }
    }
}

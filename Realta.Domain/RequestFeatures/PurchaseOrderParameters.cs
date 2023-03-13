namespace Realta.Domain.RequestFeatures
{
    public class PurchaseOrderParameters : RequestParameters
    {
        public string? Keyword { get; set; }
        public int? Status { get; set; }
        public string OrderBy { get; set; } = "PoheNumber";
    }
}

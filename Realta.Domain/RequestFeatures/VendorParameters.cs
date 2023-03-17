namespace Realta.Domain.RequestFeatures
{
    public class VendorParameters : RequestParameters
    {
        public string? Keyword { set; get; }
        public string OrderBy { get; set; } = "VendorName";
    }
}

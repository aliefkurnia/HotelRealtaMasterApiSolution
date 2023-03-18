
namespace Realta.Domain.RequestFeatures
{
    public class StocksParameters : RequestParameters
    {
        public string? SearchTerm { get; set; }
        public string OrderBy { get; set; } = "stock_name";
    }
}

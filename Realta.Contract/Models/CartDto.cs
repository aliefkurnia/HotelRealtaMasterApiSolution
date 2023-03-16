using System.Text.Json.Serialization;

namespace Realta.Contract.Models
{
    public class CartDto
    {
        public int CartId { get; set; }
        //create cart param
        public int CartVeproId { get; set; }
        public int CartEmpId { get; set; }
        //updateqty param
        public short CartOrderQty { get; set; }
        //end create cart param
        
        //show cart
        public string VendorName { get; set; }
        public string StockName { get; set; }
        public decimal VeproPrice { get; set; }
        
        public decimal Subtotal
        {
            get { return VeproPrice * CartOrderQty; }
        }
    }
}

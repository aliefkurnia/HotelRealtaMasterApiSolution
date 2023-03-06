using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Realta.Domain.Entities
{
    [Table("Purchasing.Cart")]
    public class Cart
    {
        [Key]
        public int CartId { get; set; }

        [ForeignKey("Purchasing.VendorProduct")]
        public int CartVeproId { get; set; }
        public int VendorId { get; set; }
        public int StockId { get; set; }
        public string VendorName { get; set; }
        public string StockName { get; set; }
        public decimal VeproPrice { get; set; }

        [ForeignKey("Hr.Employee")]
        public int CartEmpId { get; set; }
        public short CartOrderQty { get; set; }

        public DateTime CartModifiedDate { get; set; }

    }
}

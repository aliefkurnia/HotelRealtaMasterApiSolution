using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Domain.Entities
{
    [Table("Purchasing.stocks")]
    public class Stocks
    {
        [Key]
        public int? StockId { get; set; }
        public string StockName { get; set; }
        public string? StockDesc { get; set; }
        public Int16 StockQty { get; set; }
        public Int16 StockReorderPoint { get; set; }
        public Int16 StockUsed { get; set; }
        public Int16 StockScrap { get; set; }
        public string? StockSize { get; set; }
        public string? StockColor { get; set; }
        public DateTime? StockModifiedDate { get; set; }

    }
}

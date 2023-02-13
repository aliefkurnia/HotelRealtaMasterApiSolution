using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Contract
{
    public class StocksDto
    {
        public int? StockId { get; set; }
        public string StockName { get; set; }
        public string? StockDescription { get; set; }
        public Int16 StockQuantity { get; set; } 
        public Int16 StockReorderPoint { get; set; }
        public Int16? StockUsed { get; set; }
        public Int16? StockScrap { get; set; }
        public string? StockSize { get; set; }
        public string? StockColor { get; set; }
        public DateTime? StockModifiedDate { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Contract.Models
{
    public class StocksDto
    {
        public int? StockId { get; set; }

        [Required]
        public string StockName { get; set; }
        public string? StockDescription { get; set; }

        [Required]
        public short StockQuantity { get; set; }

        [Required]
        public short StockReorderPoint { get; set; }
        public short StockUsed { get; set; }
        public short StockScrap { get; set; }

        [MaxLength(25, ErrorMessage = "StockSize cannot be longer than 25")]
        public string? StockSize { get; set; }

        [MaxLength(15, ErrorMessage = "StockColor cannot be longer than 15")]
        public string? StockColor { get; set; }
        public DateTime? StockModifiedDate { get; set; }

    }
}

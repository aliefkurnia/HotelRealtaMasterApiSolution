using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Contract.Models
{
    public class StockDetailDto
    {
        public int? StodId { get; set; }
        public int? StodStockId { get; set; }


        public string? StodBarcodeNumber { get; set; }

        public string? StodStatus { get; set; }
        public string? StodNotes { get; set; }

        public int? StodFaciId { get; set; }

        public string? StodPoNumber { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace Realta.Contract
{
    public class StockDetailDto
    {
        public int? stod_id { get; set; }
        public int? stod_stock_id { get; set; }


        public string? stod_barcode_number { get; set; }

        public string? stod_status { get; set; }
        public string? stod_notes { get; set; }

        public int? stod_faci_id { get; set; }

        public int? stod_pohe_id { get; set; }
    }
}

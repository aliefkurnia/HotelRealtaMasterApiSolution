using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Domain.Entities
{
    [Table("Stocks")]
    public class Stocks
    {
        [Key]
        public int? stock_id { get; set; }
        public string stock_name { get; set; }
        public string? stock_description { get; set; }
        public Int16 stock_quantity { get; set; }
        public Int16 stock_reorder_point { get; set; }
        public Int16? stock_used { get; set; }
        public Int16? stock_scrap { get; set; }
        public string? stock_size { get; set; }
        public string? stock_color { get; set; }
        public DateTime? stock_modified_date { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Domain.Entities
{
    [Table("Purchasing.stock_photo")]
    public class StockPhoto
    {
        [Key]
        public int? spho_id { get; set; }
        public string? spho_thumbnail_filename { get; set; }
        public string? spho_photo_filename { get; set; }
        public bool? spho_primary { get; set; }
        public string? spho_url { get; set; }
        [ForeignKey("stocks")]
        public int? spho_stock_id { get; set; }

    }
}

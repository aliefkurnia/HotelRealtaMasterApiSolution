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
        public int? SphoId { get; set; }
        public string? SphoThumbnailFilename { get; set; }
        public string? SphoPhotoFileName { get; set; }
        public bool? SphoPrimary { get; set; }
        public string? SphoUrl { get; set; }
        [ForeignKey("stocks")]
        public int? SphoStockId { get; set; }
        public string? StockName { get; set; }

    }
}

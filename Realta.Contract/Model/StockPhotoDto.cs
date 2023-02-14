using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Contract
{
    public class StockPhotoDto
    {
        public int? spho_id { get; set; }

        [MaxLength(50, ErrorMessage = "spho_thumbnail_filename cannot be longer than 50")]
        public string? spho_thumbnail_filename { get; set; }

        [MaxLength(50, ErrorMessage = "spho_photo_filename cannot be longer than 50")]
        public string? spho_photo_filename { get; set; }
        public bool? spho_primary { get; set; }
        public string? spho_url { get; set; }
        public int? spho_stock_id { get; set; }
    }
}

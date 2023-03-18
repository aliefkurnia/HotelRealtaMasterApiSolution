using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Contract.Models
{
    public class StockPhotoGroupDto
    {
        public List<IFormFile>? AllFoto { get; set; }
    }

    public class StockPhotoDto
    {
        public int? SphoId { get; set; }
        public string? SphoThumbnailFilename { get; set; }
        public string? SphoPhotoFileName { get; set; }
        public bool? SphoPrimary { get; set; }
        public string? SphoUrl { get; set; }
        public string? StockName { get; set; }
    }
}

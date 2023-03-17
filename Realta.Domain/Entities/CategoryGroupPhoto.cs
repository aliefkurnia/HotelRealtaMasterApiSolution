using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Domain.Entities
{
    public class CategoryGroupPhoto
    {
        public int PhotoId { get; set; }
        public string? PhotoFilename { get; set; }
        public short? PhotoFileSize { get; set; }
        public string? PhotoFileType { get; set; }
        public int? PhotoCategoryGroupId { get; set; }
        public int? PhotoPrimary { get; set; }
        public string? PhotoOriginalFilename { get; set; }
    }
}

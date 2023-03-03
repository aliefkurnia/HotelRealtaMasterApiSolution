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
}

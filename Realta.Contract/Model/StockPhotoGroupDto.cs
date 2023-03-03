using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Contract.Model
{
    public class StockPhotoGroupDto
    {
        public List<IFormFile>? AllFoto { get; set; }
    }
}

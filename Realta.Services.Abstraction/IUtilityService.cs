using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Services.Abstraction
{
    public interface IUtilityService
    {
        string UploadSingleFile(IFormFile formFile);
    }
}

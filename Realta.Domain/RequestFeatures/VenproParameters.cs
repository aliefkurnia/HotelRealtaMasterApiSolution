using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Domain.RequestFeatures
{
    public class VenproParameters : RequestParameters
    {
        public string? Keyword { set; get; }
        public string OrderBy { get; set; } = "vendorName";
    }
}

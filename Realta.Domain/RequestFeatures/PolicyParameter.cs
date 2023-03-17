using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Domain.RequestFeatures
{
    public class PolicyParameter : RequestParameters
    {
        public string? SearchTerm { get; set; }

        public string OrderBy { get; set; } = "PoliId";
    }
}

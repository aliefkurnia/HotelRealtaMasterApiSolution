using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Domain.RequestFeatures
{
    public class PriceItemsParameters : RequestParameters
    {
        public uint MinStock { get; set; }
        public uint MaxStock { get; set; } = int.MaxValue;

        public bool ValidateStockRange => MaxStock > MinStock;

        public string? SearchTerm { get; set; }

        public string OrderBy { get; set; } = "pritName";
    }
}

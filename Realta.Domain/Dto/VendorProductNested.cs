using Realta.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Domain.Dto
{
    public class VendorProductNested
    {
        public int VeproVendorId { get; set; }
        public string VendorName { get; set; }
        public virtual ICollection<VendorProduct>? VendorProducts { get; set; }


    }
}

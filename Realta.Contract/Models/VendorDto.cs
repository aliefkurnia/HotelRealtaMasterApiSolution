using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Contract.Models
{
    public class VendorDto
    {
        public int VendorEntityId { get; set; }
        public string VendorName { get; set; }
        public bool VendorActive { get; set; }
        public bool VendorPriority { get; set; }
        public DateTime VendorRegisterTime { get; set; }
        public string VendorWeburl { get; set; }
        public DateTime VendorModifiedDate { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Domain.Entities
{
    [Table("Vendor")]
    public class Vendor
    {
        [Key]
        public int VendorEntityId { get; set; }
        public string VendorName { get; set; }
        public bool VendorActive { get; set; }
        public bool VendorPriority { get; set; }
        public DateTime VendorRegisterTime { get; set; }
        public string VendorWeburl { get; set; }
        public DateTime VendorModifiedDate { get; set; }

    }
}

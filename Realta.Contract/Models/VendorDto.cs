using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Contract.Models
{
    public class VendorDto
    {
        public int vendor_entity_id { get; set; }
        public string vendor_name { get; set; }
        public bool vendor_active { get; set; }
        public bool vendor_priority { get; set; }
        public DateTime vendor_register_time { get; set; }
        public string vendor_weburl { get; set; }
        public DateTime vendor_modified_date { get; set; }
    }
}

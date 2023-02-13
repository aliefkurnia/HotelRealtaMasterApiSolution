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
        public int vendor_id { get; set; }
        public string vendor_name { get; set; }
        public bool vendor_active { get; set; }
        public bool vendor_priority { get; set; }
        public DateTime vendor_register_time { get; set; }
        public string vendor_weburl { get; set; }
        public DateTime vendor_modified_date { get; set; }

    }
}

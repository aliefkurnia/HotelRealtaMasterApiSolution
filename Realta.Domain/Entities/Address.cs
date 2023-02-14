using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Types;
namespace Realta.Domain.Entities
{
    [Table("Address")]
    public class Address
    {
        [Key]
        public int addr_id { get; set; }
        [Required]
        public string addr_line1 { get; set; }
        public string addr_line2 { get; set;}
        public string addr_postal_code { get; set; }
        public string addr_spatial_location { get; set; }
        [ForeignKey("addr_prov_id")]
        public int addr_prov_id { get; set; }
    }
}

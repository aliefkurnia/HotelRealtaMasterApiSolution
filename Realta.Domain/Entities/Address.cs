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
        public int AddrId { get; set; }
        [Required]
        public string AddrLine1 { get; set; }
        public string? AddrLine2 { get; set;}
        public  string AddrCity { get; set; }
        public string? AddrPostalCode { get; set; }
        public string? AddrSpatialLocation { get; set; }
        [ForeignKey("addr_prov_id")]
        public int AddrProvId { get; set; }
    }
}

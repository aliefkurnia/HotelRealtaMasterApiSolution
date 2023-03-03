using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Domain.Entities
{
    [Table("Price_Items")]
    public class PriceItems
    {
        [Key]
        public int PritId { get; set; }
        [Required]
        public string PritName { get; set; }
        [Required]
        public decimal PritPrice { get; set; }

        public string PritDescription { get; set; }
        [Required]
        public string PritType { get; set; }
        public string PritIconUrl { get; set; }


        public DateTime PritModifiedDate { get; set; }
    }
}

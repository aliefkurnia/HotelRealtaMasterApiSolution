using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Contract.Models
{
    public class PriceItemsDto
    {
        public int PritId { get; set; }
        public string PritName { get; set; }
        public decimal PritPrice { get; set; }

        public string PritDescription { get; set; }
        public string PritType { get; set; }
        public string PritIconUrl { get; set; }
        public DateTime PritModifiedDate { get; set; }
    }
}

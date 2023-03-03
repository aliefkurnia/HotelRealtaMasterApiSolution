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
        [Key]
        public int PritId { get; set; }
        [Required]
        public string PritName { get; set; }
        [Required]
        public decimal PritPrice { get; set; }

        public string PritDescription { get; set; }
        private string _PritType;
        [Required]
        public string PritType
        {
            get { return _PritType; }
            set
            {
                if (value == "SNACK" || value == "FACILITY" || value == "SOFTDRINK" || value == "FOOD" || value == "SERVICE")
                {
                    _PritType = value;
                }
                else
                {

                    throw new ValidationException("Input harus berupa SNACK, FACILITY, SOFTDRINK, FOOD, atau SERVICE.");
                }
            }
        }
        public string PritIconUrl { get; set; }
        public DateTime PritModifiedDate { get; set; }
    }
}

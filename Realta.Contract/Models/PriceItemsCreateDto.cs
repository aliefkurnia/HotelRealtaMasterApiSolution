using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Contract.Models
{
    public class PriceItemsCreateDto
    {

        [Display(Name = "Price Items Name")]
        [Required]
        [StringLength(50, ErrorMessage = "Price Items Name cannot be longer than 50")]
        public string PritName { get; set; }
        
        [Display(Name = "Price Items Price")]
        [Required]
        public decimal PritPrice { get; set; }

        [Display(Name = "Price Items Description")]
        [Required]
        public string PritDescription { get; set; }

        private string _PritType;
        [Display(Name = "Price Items Type")]
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


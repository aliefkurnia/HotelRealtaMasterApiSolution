using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Contract.Models
{
    public class Price_ItemsDto
    {
        [Key]
        public int prit_id { get; set; }
        [Required]
        public string prit_name { get; set; }
        [Required]
        public decimal prit_price { get; set; }

        public string prit_description { get; set; }
        private string _prit_type;
        [Required]
        public string prit_type
        {
            get { return _prit_type; }
            set
            {
                if (value == "SNACK" || value == "FACILITY" || value == "SOFTDRINK" || value == "FOOD" || value == "SERVICE")
                {
                    _prit_type = value;
                }
                else
                {

                    throw new ValidationException("Input harus berupa SNACK, FACILITY, SOFTDRINK, FOOD, atau SERVICE.");
                }
            }
        }

        public DateTime prit_modified_date { get; set; }
    }
}

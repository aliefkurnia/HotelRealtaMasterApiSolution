using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Contract.Models
{
    public class Category_GroupDto
    {
        [Key] public int cagro_id { get; set; }
        [Required] public string cagro_name { get; set; }
        public string cagro_description { get; set; }
        private string _cagro_type;
        [Required]
        public string cagro_type
        {
            get { return _cagro_type; }
            set
            {
                if (value == "category" || value == "service" || value == "facility")
                {
                    _cagro_type = value;
                }
                else
                {
                    throw new ArgumentException("Input harus berupa category,service,facility");
                }
            }
        }
        public string cagro_icon { get; set; }
        public string cagro_icon_url { get; set; }
    }
}

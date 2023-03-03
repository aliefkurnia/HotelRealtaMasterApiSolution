using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Domain.Entities
{
    [Table("Category_Group")]
    public class CategoryGroup
    {
        [Key] public int CagroId { get; set; }
        [Required] public string CagroName { get; set; }
        public string CagroDescription { get; set;}
        [Required]
        public string CagroType { get; set; }
        public string CagroIcon { get; set; }
        public string CagroIconUrl { get; set; }
    }
}

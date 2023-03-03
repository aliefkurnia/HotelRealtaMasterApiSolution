using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Contract.Models
{
    public class PriceItemsPhotoGroupDto
    {

        [Required]
        public PriceItemsCreateDto? PriceItemsForCreateDto { get; set; }


        public List<IFormFile>? AllPhotos { get; set; }
    }
}

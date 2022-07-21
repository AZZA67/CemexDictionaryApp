using CemexDictionaryApp.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CemexDictionaryApp.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [ForeignKey("_ProductType")]
        public int ProductTypeId { get; set; }
        public ProductType _ProductType { get; set; }
        [Required(ErrorMessage = "Please choose profile image")]
        [Display(Name = "Product Picture")]
        public IFormFile ProductImage { get; set; }
     
    }
}

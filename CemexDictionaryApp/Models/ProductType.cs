using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CemexDictionaryApp.Models
{
    public class ProductType
    {
        public int Id { get; set; }
        public string Type { get; set; }
        List<Product> products { get; set; }
    }
}

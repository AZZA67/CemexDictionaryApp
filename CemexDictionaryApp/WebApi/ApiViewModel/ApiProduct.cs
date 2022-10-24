using CemexDictionaryApp.Models;
using CemexDictionaryApp.WebApi.ApiViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CemexDictionaryApp.WebApi.ApiModels
{
    public class ApiProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Image { get; set; }
        public string Status { get; set; }
        public string ProductType { get; set; }
    }

    public class ApiProductMapping
    {
        public static List<ApiProduct> Mapping(List<Product> products)
        {
            if(products!=null)
            {
                List<ApiProduct> _products = new();
                foreach (var item in products)
                {
                    ApiProduct p = new()
                    {
                        Id = item.Id,
                        Description = item.Description,
                        Image = string.Format(ServerConfig.ServerPath + "/images/Products/" + item.Image),
                        Name = item.Name,
                        Status = item.Status,
                        Type = item.Type,
                        ProductType = item.productType.Type
                    };
                    _products.Add(p);
                }
                return _products;
            }
            return null;
        }

        public static ApiProduct MappingByObject(Product product)
        {
            if (product != null)
            {
                ApiProduct _apiProduct = new()
                {
                    Id = product.Id,
                    Description = product.Description,
                    Image = string.Format(ServerConfig.ServerPath + "/images/Products/" + product.Image),
                    Name = product.Name,
                    Status = product.Status,
                    Type = product.Type,
                    ProductType = product.productType.Type
                };
                return _apiProduct;
            }
            return null;
        }
    }
 }

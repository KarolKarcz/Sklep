using API.Entities.ProductEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class ProductDto
    {
        public string Description { get; set; }
        public string TechnicalData { get; set; }
        public List<Product> RelatedProducts { get; set; }
        public List<Photo> ProductPhotos { get; set; }
        public List<string> Tags { get; set; }
    }
}

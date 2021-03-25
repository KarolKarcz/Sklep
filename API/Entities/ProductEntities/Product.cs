using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities.ProductEntities
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Description { get; set; }
        public string TechnicalData { get; set; }
        public List<Product> RelatedProducts { get; set; }
        public List<Photo> ProductPhotos { get; set; }
        public List<string> Tags { get; set; }
    }
}

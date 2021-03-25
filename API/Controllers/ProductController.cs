using API.Controllers.Helpers;
using API.Data;
using API.DTOs;
using API.Entities.ProductEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public ProductController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            List<Product> products = await _dataContext.Products.ToListAsync();

            if (products.Count == 0)
                return BadRequest("Product list is empty");

            return products;
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            Product product = await _dataContext.Products.FindAsync(id);

            if (product == null)
                return BadRequest("Product does not exist");

            return product;
        }

        [Authorize]
        [HttpPost("{id}")]
        public async Task<ActionResult> UpdateProduct(ProductDto productDto, int id)
        {
            Product product = await _dataContext.Products.FindAsync(id);

            if (product == null)
                return BadRequest("Product does not exist");

            ConvertDtosToEntities.ConvertProduct(ref product, productDto);

            _dataContext.Products.Update(product);

            await _dataContext.SaveChangesAsync();

            return Ok();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            Product product = await _dataContext.Products.FindAsync(id);

            if (product == null)
                return BadRequest("Product does not exist");

            _dataContext.Products.Remove(product);

            return Ok();
        }
    }
}

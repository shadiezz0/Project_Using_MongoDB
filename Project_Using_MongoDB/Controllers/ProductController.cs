using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_Using_MongoDB.Models;

namespace Project_Using_MongoDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepos _productService;
        public ProductController(IProductRepos productService)
        {
            _productService = productService;
        }
        // GET: api/Product
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var products = await _productService.GetAllAsyc();
            return Ok(products);
        }

        // GET api/ProductController/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var product = await _productService.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        // POST api/ProductController
        [HttpPost]
        public async Task<IActionResult> Post(Product product)
        {
            product.CategoryName = null;
            await _productService.CreateAsync(product);
            return Ok("created successfully");
        }

        // PUT api/ProductController/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] Product newProduct)
        {
            newProduct.CategoryName = null;
            var product = await _productService.GetById(id);
            if (product == null)
                return NotFound();
            await _productService.UpdateAsync(id, newProduct);
            return Ok("updated successfully");
        }

        // DELETE api/ProductController/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var product = await _productService.GetById(id);
            if (product == null)
                return NotFound();
            await _productService.DeleteAysnc(id);
            return Ok("deleted successfully");
        }



    }
}

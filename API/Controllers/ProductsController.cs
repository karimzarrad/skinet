using API.RequestHelper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IGenericRepository<Product> repo) : BaseApiController
    {
       

       
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts([FromQuery]ProductSpecParams specparams)
        {
            var spec = new SpecificationsProduct(specparams);
            
            return await CreatePagedResult(repo,spec,specparams.PageIndex,specparams.PageSize);
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await repo.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return product;
        }
        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            repo.add(product);
            if(await repo.saveallasync())
            {
                return CreatedAtAction("GetProduct", new { id = product.Id }, product);
            }
            return BadRequest("problem in creating product");
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateProduct(int id, Product product)
        {
            if ((product.Id != id) || !ProductExisits(id))
                return BadRequest("cannot update this product");
           repo.update(product);
            if (await repo.saveallasync())
            {
                return NoContent();
            }
            return BadRequest("problem updating product");
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var prod = await repo.GetByIdAsync(id);
            if (prod == null)
                return NotFound();
           repo.remove(prod);
            if(await repo.saveallasync())
            return NoContent();
            return BadRequest("problem deleting product");
        }
        private bool ProductExisits(int id)
        {
            return repo.Exists(id);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetBrandsAsync()
        {
            var spec = new BrandListSpec();
            return Ok(await repo.GetAllAsync(spec));
        }
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetTypesAsync()
        {
            var spec = new TypeListSpec();
            return Ok(await repo.GetAllAsync(spec));
        }

    }
}

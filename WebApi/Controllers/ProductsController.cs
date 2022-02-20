#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Models;
using WebApi.Models.Entities;

namespace WebApi.Controllers
{   
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly SqlContext _context;

        public ProductsController(SqlContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CreateProductModel>>> GetProducts()
        {
            var items = new List<CreateProductModel>();
            foreach (var item in await _context.Products.Include(x => x.Category).ToListAsync())
            {
                items.Add(new CreateProductModel(item.CategoryName, item.Name, item.Price, item.CategoryId));
            }
            return items;
            //return await _context.Products.ToListAsync();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductEntity>> GetProductEntity(int id)
        {
            var productEntity = await _context.Products.Include(x => x.Category).FirstOrDefaultAsync(x => x.Id == id); 

            if (productEntity == null)
            {
                return NotFound();
            }

            return new ProductEntity(productEntity.Id, productEntity.CategoryName, productEntity.Price, productEntity.Name, productEntity.CategoryId);
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductEntity(int id, CreateProductModel model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            var productEntity = await _context.Products.FindAsync(id);
            if (productEntity == null)
            {
                return NotFound();
            }
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Name == model.CategoryName);
            if (category != null)
            {
                productEntity.CategoryId = category.Id;
                productEntity.CategoryName = model.Name;
                productEntity.Price = model.Price;
                _context.Entry(productEntity).State = EntityState.Modified;
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductEntityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();

        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductEntity>> PostProductEntity(CreateProductModel model)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Name == model.CategoryName);
            if (category != null)
            {
                var product = new ProductEntity()
                {
                    CategoryId = category.Id,
                    CategoryName = model.CategoryName,
                    Name = model.Name,
                    Price = model.Price
                };
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                //return CreatedAtAction("GetProductEntity", new { id = product.Id }, product);
                return Ok();
            }
            else
                return BadRequest();
            
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductEntity(int id)
        {
            var productEntity = await _context.Products.FindAsync(id);
            if (productEntity == null)
            {
                return NotFound();
            }

            _context.Products.Remove(productEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductEntityExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}

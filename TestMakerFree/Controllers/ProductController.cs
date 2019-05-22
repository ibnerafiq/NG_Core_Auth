using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestMakerFree.Data;
using TestMakerFree.Models;

namespace TestMakerFree.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public ProductController(ApplicationDbContext db)
        {
            _db = db;
        }
        // GET: api/Product
        [HttpGet("[action]")]
        [Authorize(Policy = "RequireLoggedIn")]
        public IActionResult GetProducts()
        {
            return Ok(_db.Products.ToList());
        }

        

        // POST: api/Product
        [HttpPost("[action]")]
        [Authorize(Policy = "RequireAdministratorRole")]
        public async Task<IActionResult> PostProduct([FromBody] ProductModel model)
        {
            var podel = new ProductModel() { Name = model.Name, Description = model.Description, ImageUrl = model.ImageUrl, OutOfStock = model.OutOfStock, Price = model.Price};
            await _db.Products.AddAsync(podel);
            await _db.SaveChangesAsync();
            return Ok();
        }
        

        // PUT: api/Product/5
        [HttpPut("[action]/{id}")]
        [Authorize(Policy = "RequireAdministratorRole")]
        public async Task<IActionResult> UpdateProduct([FromRoute]int id, [FromBody] ProductModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var findProduct = _db.Products.FirstOrDefault(aa => aa.ProductId == id);
            if (findProduct == null)
            {
                return NotFound();
            }
            findProduct.ImageUrl = model.ImageUrl;
            findProduct.Name = model.Name;
            findProduct.OutOfStock = model.OutOfStock;
            findProduct.Price = model.Price;
            findProduct.Description = model.Description;
            _db.Entry(findProduct).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return Ok(new JsonResult("The product with id:" + id + " has been updated"));
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("[action]/{id}")]
        [Authorize(Policy = "RequireAdministratorRole")]
        public async Task<IActionResult> DeleteProduct([FromRoute]int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var findProduct = _db.Products.FirstOrDefault(aa => aa.ProductId == id);
            if (findProduct == null)
            {
                return NotFound();
            }

            _db.Products.Remove(findProduct);
            await _db.SaveChangesAsync();
            return Ok(new JsonResult("The product with id:" + id + " has been deleted"));

        }
    }
}

using System.Linq;
using System.Threading.Tasks;
using HBWeekOneAPI.Constants;
using HBWeekOneAPI.Dtos.Product;
using HBWeekOneAPI.EntityFramework;
using HBWeekOneAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HBWeekOneAPI.Controllers
{
    /// <summary>
    /// Product Controller
    /// </summary>
    [Route("api/Products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        /// <summary>
        /// Return List of products
        /// </summary>
        /// <param name="name">Optional product name parameter for filtering</param>
        /// <returns>list of products</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProductList([FromQuery]string name)
        {
            return name != null ? Ok(_context.Products.Where(x=>x.Name.Contains(name))) : Ok(await _context.Products.ToListAsync());
        }
        
        /// <summary>
        /// Return product by given product Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Single product</returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProduct([FromRoute]int id)
        {
            var product = await _context.Products.FindAsync(id);
            return product != null ? Ok(product) : NotFound(new {message=Message.ProductNotFound});
        }

        /// <summary>
        /// Create product By given parameters
        /// </summary>
        /// <param name="productCreateDto"></param>
        /// <returns>Http response code</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateProduct([FromBody]ProductCreateDto productCreateDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Name.ToLower() == productCreateDto.Name.ToLower());
            if (product != null)
            {
                return BadRequest(new {message = Message.ProductAlreadyAdded});
            }
            await _context.Products.AddAsync(new Product
            {
                Name = productCreateDto.Name
            });
            await _context.SaveChangesAsync();
            return NoContent();
        }
        
        /// <summary>
        /// Update product by given product name and product Id
        /// </summary>
        /// <param name="id">Product Id</param>
        /// <param name="productUpdateDto">New Product Name</param>
        /// <returns>Updated Product</returns>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateProduct(int id,[FromBody]ProductUpdateDto productUpdateDto )
        {   
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound(new {message = Message.ProductNotFound});
            if (product.Name.ToLower()==productUpdateDto.Name.ToLower())
            {
                return BadRequest(new {message =Message.ProductNameNotUnique});
            }
            product.Name = productUpdateDto.Name;
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return Ok(product);

        }
        
        /// <summary>
        /// Delete Product by given product Id
        /// </summary>
        /// <param name="id">Product Id</param>
        /// <returns>Http response code</returns>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound(new {message = Message.ProductNotFound});
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
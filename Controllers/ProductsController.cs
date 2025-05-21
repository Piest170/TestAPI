using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestAPI.Data;
using TestAPI.Models;

namespace TestAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProductsController(AppDbContext context)
    {
        _context = context;
    }

    // Get All Data that Sort by Price
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
    {
        try
        {
            var products = await _context.Products.OrderBy(o => o.Price).ToListAsync();
            return Ok(products);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occured while retriveing products. " + ex.Message);
        }
    }
    
    // Get Data by ID
    [HttpGet("{id}")]
    public async Task<ActionResult<IEnumerable<Product>>> GetProductById(int id) {
        try
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound($"Product with ID {id} was not found.");
            }
            return Ok(product);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occured while retriving the product. " + ex.Message);
        }
    }

    // Save Data
    [HttpPost]
    public async Task<IActionResult> CreateProduct(Productdto dto)
    {
        try
        {
            var product = new Product
            {
                Name = dto.Name,
                Price = dto.Price,
                Stock = dto.Stock,
                Description = dto.Description
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occured while create the product. " + ex.Message);
        }
    }
}
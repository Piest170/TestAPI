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
    public async Task<ActionResult<IEnumerable<Product>>> GetProductById(int id)
    {
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
                Quantity = dto.Quantity,
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

    // // Assignment2 Function that Calculate a total revenue of an order.
    // [HttpGet("total-revenue")]
    // public async Task<ActionResult<IEnumerable<double>>> Calculate()
    // {
    //     try
    //     {
    //         /* ปัญหาที่พบคือ ถ้าสมมุติมีสินค้ามากขึ้นจะทำให้เกิดการส่งข้อมูลที่ช้า ดังนั้น การเปลี่ยนแก้ไข จาก for loop มาในส่วนนี้จะทำให้การทำงานการส่งข้อมูลที่เร็ว 
    //             และรองรับข้อมูลได้มากกว่าการส่งข้อมูลที่ละชิ้นจาก for loop โดยที่จะทำการคำนวณข้อมูลใน array ที่เรียงตามในแต่ละชุด array 
    //             ในกรณีของ C# เราสามารถดึงข้อมูลจากฐาน Database โดยตรงได้จาก การใช้ _context.Products.ToListAsync() 
    //         */

    //         // optimized code
    //         var products = await _context.Products.ToListAsync();
    //         if (products.Count == 0)
    //         {
    //             return BadRequest("No order items.");
    //         }
    //         double totalrevenue = 0;
    //         foreach (var product in products)
    //         {
    //             totalrevenue = + product.Price;
    //         }

    //         return Ok(totalrevenue);
    //     }
    //     catch (Exception ex)
    //     {
    //         return StatusCode(500, "An error occurred while cauculate. " + ex.Message);
    //     }
    // }
}
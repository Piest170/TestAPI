using System.ComponentModel.DataAnnotations;

namespace TestAPI.Models;

// Function that use when add data then Generate ID Automatically when add data.
public class Productdto
{
    [Required]
    [StringLength(100)]
    public string? Name { get; set; }
    public double Price { get; set; }
    public int Quantity { get; set; }
    public string? Description { get; set; }
}
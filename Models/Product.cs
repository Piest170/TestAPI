using System.ComponentModel.DataAnnotations;

namespace TestAPI.Models;

//Function that use for get Data.
public class Product
{
    public int Id { get; set; }
    [Required]
    [StringLength(100)]
    public string? Name { get; set; }
    public double Price { get; set; }
    public int Stock { get; set; }
    public string? Description { get; set; }
}
using System.ComponentModel.DataAnnotations;

namespace Task1_RestfulApi;
public class Product
{
    [Required]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Color { get; set; }
    [Required]
    public decimal Price { get; set; }
}

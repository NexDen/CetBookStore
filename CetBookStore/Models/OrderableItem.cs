using System.ComponentModel.DataAnnotations;

namespace CetBookStore.Models;

public class OrderableItem
{
    [Key] public int Id { get; set; }

    [Required]
    [StringLength(200, MinimumLength = 2)]
    public string Name { get; set; } = null!;

    public decimal Price { get; set; }
    public decimal? PreviousPrice { get; set; }
    public bool IsInSale   { get; set; }
    
    public int CategoryId { get; set; }

    public virtual Category? Category { get; set; }

    public virtual List<Comment>? Comments { get; set; }
}
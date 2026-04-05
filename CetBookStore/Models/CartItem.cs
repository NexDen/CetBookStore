namespace CetBookStore.Models;

public class CartItem
{
    public int ItemId { get; set; }
    public string Name { get; set; } = null!;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}
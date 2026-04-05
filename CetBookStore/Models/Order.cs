namespace CetBookStore.Models;

public class Order
{
    public int Id { get; set; }
    public string UserId { get; set; } = null!;
    public DateTime OrderDate { get; set; }
    public List<OrderItem> Items { get; set; } = new();
}

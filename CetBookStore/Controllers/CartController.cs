using System.Security.Claims;
using System.Text.Json;
using CetBookStore.Data;
using CetBookStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CetBookStore.Controllers;

[Authorize]
public class CartController : Controller
{
    private const string CartSessionKey = "Cart";
    private readonly ApplicationDbContext _context;

    public CartController(ApplicationDbContext context)
    {
        _context = context;
    }

    private List<CartItem> GetCart()
    {
        var cartJson = HttpContext.Session.GetString(CartSessionKey);
        return cartJson == null
            ? new List<CartItem>()
            : JsonSerializer.Deserialize<List<CartItem>>(cartJson)!;
    }

    private void SaveCart(List<CartItem> cart)
    {
        HttpContext.Session.SetString(CartSessionKey, JsonSerializer.Serialize(cart));
    }

    public IActionResult Index()
    {
        return View(GetCart());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddToCart(int itemId)
    {
        var item = await _context.Books.FindAsync(itemId);
        if (item == null) return NotFound();

        var cart = GetCart();
        var existing = cart.FirstOrDefault(i => i.ItemId == itemId);
        if (existing != null)
        {
            existing.Quantity++;
        }
        else
        {
            cart.Add(new CartItem { ItemId = item.Id, Name = item.Name, Price = item.Price, Quantity = 1 });
        }
        SaveCart(cart);

        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Remove(int itemId)
    {
        var cart = GetCart();
        var item = cart.FirstOrDefault(i => i.ItemId == itemId);
        if (item != null) cart.Remove(item);
        SaveCart(cart);
        return RedirectToAction("Index");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Checkout()
    {
        var cart = GetCart();
        if (!cart.Any()) return RedirectToAction("Index");

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        var order = new Order
        {
            UserId = userId,
            OrderDate = DateTime.UtcNow,
            Items = cart.Select(i => new OrderItem
            {
                ItemId = i.ItemId,
                ItemName = i.Name,
                Quantity = i.Quantity,
                Price = i.Price
            }).ToList()
        };

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        HttpContext.Session.Remove(CartSessionKey);

        return RedirectToAction("Index", "Orders");
    }
}
using System.Security.Claims;
using CetBookStore.Data;
using CetBookStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CetBookStore.Controllers;

[Authorize]
public class OrdersController : Controller
{
    private readonly ApplicationDbContext _context;

    public OrdersController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> BuyNow(int itemId)
    {
        var item = await _context.Books.FindAsync(itemId);
        if (item == null) return NotFound();

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        var order = new Order
        {
            UserId = userId,
            OrderDate = DateTime.UtcNow,
            Items = new List<OrderItem>
            {
                new OrderItem { ItemId = item.Id, ItemName = item.Name, Quantity = 1, Price = item.Price }
            }
        };

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Index()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var orders = await _context.Orders
            .Include(o => o.Items)
            .Where(o => o.UserId == userId)
            .OrderByDescending(o => o.OrderDate)
            .ToListAsync();
        return View(orders);
    }
}
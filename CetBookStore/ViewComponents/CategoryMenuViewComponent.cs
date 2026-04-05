using CetBookStore.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CetBookStore.ViewComponents;

public class CategoryMenuViewComponent : ViewComponent
{
    private readonly ApplicationDbContext _context;

    public CategoryMenuViewComponent(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var categories = await _context.Categories
            .Where(c => c.IsVisibleInMenu)
            .ToListAsync();
        return View(categories);
    }
}
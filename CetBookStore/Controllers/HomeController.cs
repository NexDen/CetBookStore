using CetBookStore.Data;
using CetBookStore.Models;
using CetBookStore.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Threading.Tasks;

namespace CetBookStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext context;

        public HomeController(ApplicationDbContext context)
        {
            this.context = context;
        }


        public async Task<IActionResult> Index()
        {


            HomePageViewModel homePageViewModel = new HomePageViewModel();


           homePageViewModel.MostCommenteds =await context.Books.
                OrderByDescending(b => b.Comments.Count)
                .ThenByDescending(b => b.Name)
                .Take(3)
                .Select(b => new BookViewModel
            {
                Id = b.Id,
                Name = b.Name,
                Author = b.Author,
                Price = b.Price,
                OldPrice = b.PreviousPrice??0,
                IsInSale = b.IsInSale

                }).ToListAsync();

            homePageViewModel.NewArrivals = await context.Books
                .OrderByDescending(b => b.CreatedDate)
                .ThenBy(b => b.Name)
                .Take(3)
                .Select(b => new BookViewModel
                {
                    Id = b.Id,
                    Name = b.Name,
                    Author = b.Author,
                    Price = b.Price,
                    OldPrice = b.PreviousPrice??0,
                    IsInSale = b.IsInSale
                }).ToListAsync();


            homePageViewModel.RandomBoooks = await context.Books
                .OrderBy(b => Guid.NewGuid())
                .Take(6)
                .Select(b => new BookViewModel
                {
                    Id = b.Id,
                    Name = b.Name,
                    Author = b.Author,
                    Price = b.Price,
                    OldPrice = b.PreviousPrice??0,
                    IsInSale = b.IsInSale
                }).ToListAsync();

            return View(homePageViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

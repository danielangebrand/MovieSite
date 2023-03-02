using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;

namespace OnlineShop.Controllers
{
    public class MoviesController : Controller
    {
        private readonly AppDbContext _context;
        public MoviesController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index() => View(await _context.Movies.Include(c => c.Cinema).OrderBy(n => n.Name).ToListAsync());
        //{
        //    var model = await _context.Movies.ToListAsync();
        //    return View(model);
        //}
    }
}

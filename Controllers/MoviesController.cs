using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.Data.Services;
using System.Linq.Expressions;

namespace OnlineShop.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMoviesService _service;
        public MoviesController(IMoviesService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var m = await _service.GetAllAsync(c => c.Cinema);
            return View(await _service.GetAllAsync());
        }
        
        public async Task<IActionResult> Details(int id)
        {
            var m = await _service.GetMovieByIdAsync(id);
            return View(m);
        }

        public IActionResult Create()
        {
            ViewData["Welcome"] = "Welcome to our store";
            ViewBag.Descr = "This is the store description";
            return View();
        }
    }
}

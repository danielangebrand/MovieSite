using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.Data.Services;

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

    }
}

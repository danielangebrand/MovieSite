using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.Data.Services;
using OnlineShop.Data.ViewModels;
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

        public async Task<IActionResult> Create()
        {
            var movieDropdownsData = await _service.GetNewMovieDropdownsValues();
            ViewBag.Cinemas = new SelectList(movieDropdownsData.Cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(movieDropdownsData.Producers, "Id", "FullName");
            ViewBag.Actors = new SelectList(movieDropdownsData.Actors, "Id", "FullName");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewMovieVM m)
        {
            if (!ModelState.IsValid)
            {
                var mDropdownData = await _service.GetNewMovieDropdownsValues();
                ViewBag.Cinemas = new SelectList(mDropdownData.Cinemas, "Id", "Name");
                ViewBag.Producers = new SelectList(mDropdownData.Producers, "Id", "FullName");
                ViewBag.Actors = new SelectList(mDropdownData.Actors, "Id", "FullName");
                return View(m);
            }

            await _service.AddNewMovieAsync(m);
            return RedirectToAction(nameof(Index));
        }
    }
}

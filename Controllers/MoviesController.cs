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

        public async Task<IActionResult> Edit(int id)
        {
            var m = await _service.GetMovieByIdAsync(id);
            if (m == null) return View("NotFound");

            var response = new NewMovieVM()
            {
                Id = m.Id,
                Name = m.Name,
                Descr = m.Descr,
                Price = m.Price,
                StartDate = m.StartDate,
                EndDate = m.EndDate,
                ImgURL = m.ImageURL,
                MovieCategory = m.MovieCategory,
                CinemaId = m.CinemaId,
                ProducerId = m.ProducerId,
                ActorId = m.Actors_Movies.Select(n => n.ActorId).ToList()
            };

            var mDropdown = await _service.GetNewMovieDropdownsValues();
            ViewBag.Cinemas = new SelectList(mDropdown.Cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(mDropdown.Producers, "Id", "FullName");
            ViewBag.Actors = new SelectList(mDropdown.Actors, "Id", "FullName");

            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewMovieVM m)
        {
            if (id != m.Id) return View("NotFound");

            if (!ModelState.IsValid)
            {
                var mDropdownData = await _service.GetNewMovieDropdownsValues();

                ViewBag.Cinemas = new SelectList(mDropdownData.Cinemas, "Id", "Name");
                ViewBag.Producers = new SelectList(mDropdownData.Producers, "Id", "FullName");
                ViewBag.Actors = new SelectList(mDropdownData.Actors, "Id", "FullName");

                return View(m);
            }

            await _service.UpdateMovieAsync(m);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Filter(string search)
        {
            var mList = await _service.GetAllAsync(m => m.Cinema);
            if (!string.IsNullOrEmpty(search))
            {
                var filter = mList.Where(n => n.Name.Contains(search) || n.Descr.Contains(search)).ToList();
                return View("Index", filter);
            }

            return View("Index", mList);
        }
    }
}

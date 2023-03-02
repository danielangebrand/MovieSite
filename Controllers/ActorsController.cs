using Microsoft.AspNetCore.Mvc;
using OnlineShop.Data;
using OnlineShop.Data.Services;
using OnlineShop.Models;
using System.Diagnostics;

namespace OnlineShop.Controllers
{
    public class ActorsController : Controller
    {
        private readonly IActorsService _service;

        public ActorsController(IActorsService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()/* => View(await _service.GetAllActors().ToListAsync());*/
        {
            var model = await _service.GetAllAsync();
            return View(model);
        }
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create([Bind("FullName,ProfilePicURL,Bio")] Actor actor)
        {
            if (!ModelState.IsValid)
            {
                return View(actor);
            }
            await _service.AddAsync(actor);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(int id) => (await _service.GetByIdAsync(id)) != null ? View(await _service.GetByIdAsync(id)) : View("NotFound");

        public async Task<IActionResult> Edit(int id) => (await _service.GetByIdAsync(id)) != null ? View(await _service.GetByIdAsync(id)) : View("NotFound");
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Actor actor)
        {
            if (!ModelState.IsValid)
            {
                return View(actor);
            }
            await _service.UpdateAsync(id, actor);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id) => (await _service.GetByIdAsync(id)) != null ? View(await _service.GetByIdAsync(id)) : View("NotFound");
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await _service.GetByIdAsync(id) == null) 
                return View("NotFound");
            
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
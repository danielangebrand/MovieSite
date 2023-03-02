using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.Data.Services;

namespace OnlineShop.Controllers
{
    public class ProducersController : Controller
    {
        private readonly IProducersService _service;
        public ProducersController(IProducersService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _service.GetAllAsync();
            return View(model);
        }

        public async Task<IActionResult> Details(int id) => (await _service.GetByIdAsync(id)) != null ? View(await _service.GetByIdAsync(id)) : View("NotFound");

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create([Bind("ProfilePicURL,FullName,Bio")] Producer producer)
        {
            if (!ModelState.IsValid) return View(producer);

            await _service.AddAsync(producer);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var p = await _service.GetByIdAsync(id);
            if (p != null) return View(p);
            return View("NotFound");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProfilePicURL,FullName,Bio")] Producer producer)
        {
            if (!ModelState.IsValid) return View(producer);

            if (id == producer.Id)
            {
                await _service.UpdateAsync(id, producer);
                return RedirectToAction(nameof(Index));
            }
            return View(producer);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var p = await _service.GetByIdAsync(id);
            if (p != null) return View(p);
            return View("NotFound");
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var p = await _service.GetByIdAsync(id);
            if (p != null)
            {
                await _service.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
            return View("NotFound");
        }
    }
}

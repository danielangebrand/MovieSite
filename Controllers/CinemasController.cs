using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.Data.Services;
using OnlineShop.Data.Static;

namespace OnlineShop.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class CinemasController : Controller
    {
        private readonly ICinemasService _service;
        public CinemasController(ICinemasService service)
        {
            _service = service;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var model = await _service.GetAllAsync();
            return View(model);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Logo,Name,Descr")] Cinema cinema)
        {
            if (!ModelState.IsValid) return View(cinema);
            await _service.AddAsync(cinema);
            return RedirectToAction(nameof(Index));
        }
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var c = await _service.GetByIdAsync(id);
            if (c == null) return View("NotFound");
            return View(c);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var c = await _service.GetByIdAsync(id);
            if (c == null) return View("NotFound");
            return View(c);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Logo,Name,Descr")] Cinema c)
        {
            if (!ModelState.IsValid) return View(c);
            await _service.UpdateAsync(id, c);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var c = await _service.GetByIdAsync(id);
            if (c == null) return View("NotFound");
            return View(c);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var c = await _service.GetByIdAsync(id);
            if (c == null) return View("NotFound");

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }


    }
}

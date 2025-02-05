using Microsoft.AspNetCore.Mvc;
using HotelManagementSystem.Services;
using HotelManagementSystem.Models;

namespace HotelManagementSystem.Controllers
{
    public class MenuController : Controller
    {
        private readonly MenuApiService _menuApiService;

        public MenuController(MenuApiService menuApiService)
        {
            _menuApiService = menuApiService;
        }

        // GET: Menu
        public async Task<IActionResult> Index()
        {
            var menus = await _menuApiService.GetMenusAsync();
            return View(menus);
        }

        // GET: Menu/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var menu = await _menuApiService.GetMenuAsync(id);
            if (menu == null)
            {
                return NotFound();
            }
            return View(menu);
        }

        // GET: Menu/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Menu/Create
        [HttpPost]
        public async Task<IActionResult> Create(Menu menu)
        {
            if (ModelState.IsValid)
            {
                await _menuApiService.CreateMenuAsync(menu);
                return RedirectToAction(nameof(Index));
            }
            return View(menu);
        }

        // GET: Menu/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var menu = await _menuApiService.GetMenuAsync(id);
            if (menu == null)
            {
                return NotFound();
            }
            return View(menu);
        }

        // POST: Menu/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Menu menu)
        {
            if (id != menu.MenuId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _menuApiService.UpdateMenuAsync(id, menu);
                return RedirectToAction(nameof(Index));
            }
            return View(menu);
        }

        // GET: Menu/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var menu = await _menuApiService.GetMenuAsync(id);
            if (menu == null)
            {
                return NotFound();
            }
            return View(menu);
        }

        // POST: Menu/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _menuApiService.DeleteMenuAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

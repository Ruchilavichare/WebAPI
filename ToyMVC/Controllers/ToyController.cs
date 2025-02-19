using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using ToyMVC.Models;
using ToyMVC.Services;

namespace ToyMVC.Controllers
{
    public class ToyController : Controller
    {
        private readonly ToyService _toyService;

        public ToyController(ToyService toyService)
        {
            _toyService = toyService;
        }

        public async Task<IActionResult> Index()
        {
            var toys = await _toyService.GetHighConvertingToysAsync();
            return View(toys);
        }

        public async Task<IActionResult> TopRated()
        {
            var toys = await _toyService.GetTopRatedToysAsync();
            return View(toys);
        }

        public async Task<IActionResult> BestForCategory(string category)
        {
            var toys = await _toyService.GetToysByCategoryAsync(category);
            return View(toys);
        }

        public async Task<IActionResult> DealsOfTheDay()
        {
            var toys = await _toyService.GetDealsOfTheDayAsync();
            return View(toys);
        }
    }
}

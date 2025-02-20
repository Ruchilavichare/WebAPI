using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
            ViewData["Title"] = "Best Educational Toys for Toddlers 2025 | ToyStore";
            ViewData["Description"] = "Discover the best educational toys for toddlers in 2025, including top-rated STEM toys, creative playsets, and interactive learning experiences.";

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
        public async Task<IActionResult> Comparison()
        {
            ViewData["Title"] = "Compare Top-Rated Toys | ToyStore";
            ViewData["Description"] = "Side-by-side comparisons of the best toys, helping you find the perfect toy with the best price, reviews, and features.";

            var toys = await _toyService.GetTopRatedToysAsync();
            return View(toys);
        }
    }
}

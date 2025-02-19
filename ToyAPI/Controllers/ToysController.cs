using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToyAPI.Data;
using ToyAPI.Models;

namespace ToyAPI.Controllers
{
    [Route("api/toys")]
    [ApiController]
    public class ToysController : ControllerBase
    {
        private readonly ToyDbContext _context;

        public ToysController(ToyDbContext context)
        {
            _context = context;
        }

        [HttpGet("high-converting")]
        public async Task<ActionResult<IEnumerable<Toy>>> GetHighConvertingToys()
        {
            var toys = await _context.Toys
                .Where(t => t.IsBestSeller || t.AddedDate >= DateTime.UtcNow.AddMonths(-1))
                .OrderByDescending(t => t.ReviewsCount)
                .ThenByDescending(t => t.Rating)
                .ToListAsync();
            return Ok(toys);
        }

        [HttpGet("top-rated")]
        public async Task<ActionResult<IEnumerable<Toy>>> GetTopRatedToys()
        {
            var toys = await _context.Toys
                .Where(t => t.Rating >= 4 && t.ReviewsCount > 50)
                .OrderByDescending(t => t.Rating)
                .ThenByDescending(t => t.ReviewsCount)
                .ToListAsync();
            return Ok(toys);
        }

        [HttpGet("best-for/{category}")]
        public async Task<ActionResult<IEnumerable<Toy>>> GetToysByCategory(string category)
        {
            var toys = await _context.Toys
                .Where(t => t.Category == category)
                .OrderByDescending(t => t.Rating)
                .ToListAsync();
            return Ok(toys);
        }

        [HttpGet("deals-of-the-day")]
        public async Task<ActionResult<IEnumerable<Toy>>> GetDealsOfTheDay()
        {
            var toys = await _context.Toys
                .Where(t => t.IsDealOfTheDay)
                .OrderByDescending(t => t.DiscountedPrice)
                .ToListAsync();
            return Ok(toys);
        }
    }
}

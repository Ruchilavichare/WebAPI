using HotelManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementSystem.Controllers
{
    public class ReportsController : Controller
    {
        private readonly HotelManagementDbContext _context;

        public ReportsController(HotelManagementDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> DailySalesReport(DateTime? date)
        {
            // If no date is provided, default to today's date
            if (!date.HasValue)
            {
                date = DateTime.Today;
            }

            // Fetch orders for the specified date
            var orders = await _context.Orders
                .Include(o => o.Customer) // Include Customer details
                .Include(o => o.OrderItems) // Include OrderItems
                    .ThenInclude(oi => oi.Menu) // Include Menu details for each OrderItem
                .Where(o => o.OrderDate.Date == date.Value.Date) // Filter by date
                .ToListAsync();

            // Pass the selected date to the view
            ViewBag.SelectedDate = date.Value.ToString("yyyy-MM-dd");

            return View(orders);
        }
        public async Task<IActionResult> MenuReport()
        {
            var menus = await _context.Menus.ToListAsync();
            return View(menus);
        }
    }
}

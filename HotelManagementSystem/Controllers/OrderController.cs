using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelManagementSystem.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HotelManagementSystem.Controllers
{
    public class OrderController : Controller
    {
        private readonly HotelManagementDbContext _context;

        public OrderController(HotelManagementDbContext context)
        {
            _context = context;
        }

        // GET: Order
        public async Task<IActionResult> Index(int page = 1, int pageSize = 5)
        {
            var totalOrders = await _context.Orders.CountAsync();
            var orders = await _context.Orders
                .Include(o => o.Customer)
                .OrderByDescending(o => o.OrderDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var viewModel = new OrderListViewModel
            {
                Orders = orders,
                CurrentPage = page,
                PageSize = pageSize,
                TotalOrders = totalOrders
            };

            return View(viewModel);
        }

        // GET: Order/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Menu)
                .FirstOrDefaultAsync(m => m.OrderId == id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        public IActionResult Create()
        {
            // Convert Customers to SelectListItem
            ViewBag.Customers = _context.Customers
                .Select(c => new SelectListItem
                {
                    Value = c.CustomerId.ToString(),
                    Text = c.Name
                }).ToList();

            // Convert Menus to SelectListItem
            ViewBag.Menus = _context.Menus
                .Select(m => new SelectListItem
                {
                    Value = m.MenuId.ToString(),
                    Text = $"{m.ItemName} - {m.Price:C}"
                }).ToList();

            return View();
        }
        // POST: Order/Create
        [HttpPost]
        public async Task<IActionResult> Create(int customerId, List<int> menuIds, List<int> quantities)
        {
            // Validate CustomerId
            var customer = await _context.Customers.FindAsync(customerId);
            if (customer == null)
            {
                ModelState.AddModelError("CustomerId", "Invalid Customer selected.");
                ViewBag.Customers = _context.Customers
                    .Select(c => new SelectListItem
                    {
                        Value = c.CustomerId.ToString(),
                        Text = c.Name
                    }).ToList();
                ViewBag.Menus = _context.Menus
                    .Select(m => new SelectListItem
                    {
                        Value = m.MenuId.ToString(),
                        Text = $"{m.ItemName} - {m.Price:C}"
                    }).ToList();
                return View();
            }

            var order = new Order
            {
                CustomerId = customerId,
                OrderDate = DateTime.Now,
                TotalAmount = 0
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            decimal totalAmount = 0;
            for (int i = 0; i < menuIds.Count; i++)
            {
                if (quantities[i] > 0)
                {
                    var menu = await _context.Menus.FindAsync(menuIds[i]);
                    var orderItem = new OrderItem
                    {
                        OrderId = order.OrderId,
                        MenuId = menuIds[i],
                        Quantity = quantities[i],
                        Price = menu?.Price ?? 0
                    };
                    totalAmount += (menu?.Price ?? 0) * quantities[i];
                    _context.OrderItems.Add(orderItem);
                }
            }

            order.TotalAmount = totalAmount;
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Order/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Menu)
                .FirstOrDefaultAsync(m => m.OrderId == id);

            if (order == null)
            {
                return NotFound();
            }

            // Convert Customers to SelectListItem
            ViewBag.Customers = _context.Customers
                .Select(c => new SelectListItem
                {
                    Value = c.CustomerId.ToString(),
                    Text = c.Name,
                    Selected = order != null && c.CustomerId == order.CustomerId
                }).ToList();

            // Extract selected menu items from the order
            var selectedItems = order?.OrderItems
             .Where(oi => oi.MenuId.HasValue) 
             .ToDictionary(oi => oi.MenuId ?? 0, oi => oi.Quantity)
             ?? new Dictionary<int, int>();

            var TotalAmount = order?.TotalAmount > 0 ? order.TotalAmount  : 0;
            ViewBag.TotalAmount = TotalAmount;

            // Convert Menus to SelectListItem
            ViewBag.Menus = _context.Menus
                .Select(m => new
                {
                    Value = m.MenuId.ToString(),
                    Text = $"{m.ItemName} - {m.Price:C}",
                    EditText = selectedItems.ContainsKey(m.MenuId)
                                ? $"{selectedItems[m.MenuId]}"
                                : ""
                }).ToList();

            return View(order);
        }

        // POST: Order/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(int id, int customerId, List<int> menuIds, List<int> quantities)
        {
            // Validate CustomerId
            var customer = await _context.Customers.FindAsync(customerId);
            if (customer == null)
            {
                ModelState.AddModelError("CustomerId", "Invalid Customer selected.");
                ViewBag.Customers = _context.Customers
                    .Select(c => new SelectListItem
                    {
                        Value = c.CustomerId.ToString(),
                        Text = c.Name
                    }).ToList();
                ViewBag.Menus = _context.Menus
                    .Select(m => new SelectListItem
                    {
                        Value = m.MenuId.ToString(),
                        Text = $"{m.ItemName} - {m.Price:C}"
                    }).ToList();
                return View();
            }

            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(m => m.OrderId == id);

            if (order == null)
            {
                return NotFound();
            }

            order.CustomerId = customerId;
            order.OrderDate = DateTime.Now;
            order.TotalAmount = 0;

            // Remove existing order items
            _context.OrderItems.RemoveRange(order.OrderItems);

            // Add new order items
            decimal totalAmount = 0;
            for (int i = 0; i < menuIds.Count; i++)
            {
                if (quantities[i] > 0)
                {
                    var menu = await _context.Menus.FindAsync(menuIds[i]);
                    var orderItem = new OrderItem
                    {
                        OrderId = order.OrderId,
                        MenuId = menuIds[i],
                        Quantity = quantities[i],
                        Price = menu?.Price ?? 0
                    };
                    totalAmount += (menu?.Price ?? 0) * quantities[i];
                    _context.OrderItems.Add(orderItem);
                }
            }

            order.TotalAmount = totalAmount;
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Order/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Menu)
                .FirstOrDefaultAsync(m => m.OrderId == id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Order/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(m => m.OrderId == id);

            if (order != null)
            {
                _context.OrderItems.RemoveRange(order.OrderItems);
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
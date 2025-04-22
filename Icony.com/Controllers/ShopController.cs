using Icony.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Icony.Web.Controllers
{
    public class ShopController : Controller
    {
        private readonly AppDbContext _context;
        public ShopController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var products=await _context.Products
                                        .Include(p=>p.Category)
                                        .ToListAsync();
            return View(products);
        }
    }
}

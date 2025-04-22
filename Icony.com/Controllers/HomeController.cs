using System.Diagnostics;
using Icony.com.Models;
using Icony.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Icony.com.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var products= await _context.Products
                                        .Include(p=>p.Category).ToListAsync();
            return View(products);
        }
    }
}

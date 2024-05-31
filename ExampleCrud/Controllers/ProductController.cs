using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CRUDExample.Data;
using System.Linq;
using System.Threading.Tasks;
using ExampleCrud.Models;

namespace CRUDExample.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products=await _context.Products.ToListAsync();

            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View(new Product());
        }
        [HttpPost]
        public async Task<IActionResult> Create(Product model)
        {
            await _context.Products.AddAsync(model);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var products = _context.Products.Where(pro=>pro.Id ==  id).FirstOrDefault();
             _context.Products.Remove(products);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        
    }
}

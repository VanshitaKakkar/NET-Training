using Assignment_3.Models;
using Microsoft.AspNetCore.Mvc;

namespace Assignment_3.Controllers
{
    public class ProductController : Controller
    {
        private static List<Product> products = new()
        {
            new Product(1, "Soap", 10.50),
            new Product(2, "Shampoo", 89.80),
            new Product(3, "Dettol", 100)
        };

        [HttpGet]
        public IActionResult Index()
        {
            return View(products);
        }

        [HttpGet("Product/Details/{id}")]
        public IActionResult Details(int id)
        {
            var product = products.FirstOrDefault(p => p.ProductId == id);

            if (product == null)
                return NotFound();

            return View(product);
        }
    }
}

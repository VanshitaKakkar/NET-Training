using MVC_Assignment.Models;
using Microsoft.AspNetCore.Mvc;

namespace MVC_Assignment.Controllers
{
    public class ProductController : Controller
    {
        private static List<Product> products = new()
        {
            new Product(1, "Soap", 10.50m, "Bath soap"),
            new Product(2, "Shampoo", 89.80m, "Hair shampoo"),
            new Product(3, "Dettol", 100m, "Antiseptic liquid")
        };

        // GET: Product
        public IActionResult Index()
        {
            return View(products);
        }

        // GET: Product/Details/5
        public IActionResult Details(int id)
        {
            var product = products.FirstOrDefault(p => p.ProductId == id);
            if (product == null)
                return NotFound();

            return View(product);
        }

        // GET: Product/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            if (!ModelState.IsValid)
                return View(product);

            product.ProductId = products.Max(p => p.ProductId) + 1;
            products.Add(product);

            return RedirectToAction(nameof(Index));
        }

        // GET: Product/Edit/5
        public IActionResult Edit(int id)
        {
            var product = products.FirstOrDefault(p => p.ProductId == id);
            if (product == null)
                return NotFound();

            return View(product);
        }

        // POST: Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Product product)
        {
            if (!ModelState.IsValid)
                return View(product);

            var existing = products.FirstOrDefault(p => p.ProductId == product.ProductId);
            if (existing == null)
                return NotFound();

            existing.Name = product.Name;
            existing.Price = product.Price;
            existing.Description = product.Description;

            return RedirectToAction(nameof(Index));
        }

        // GET: Product/Delete/5
        public IActionResult Delete(int id)
        {
            var product = products.FirstOrDefault(p => p.ProductId == id);
            if (product == null)
                return NotFound();

            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = products.FirstOrDefault(p => p.ProductId == id);
            if (product != null)
                products.Remove(product);

            return RedirectToAction(nameof(Index));
        }
    }
}

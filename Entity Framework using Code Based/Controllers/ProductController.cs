using Entity_Framework.Data;
using Entity_Framework.Models;
using Microsoft.AspNetCore.Mvc;

namespace Entity_Framework.Controllers
{
    public class ProductController : Controller
    {
        private readonly MyDbContext _myDbContext;

        public ProductController(MyDbContext context)
        {
            _myDbContext = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<Product> products = _myDbContext.Products.ToList();
            return View(products);
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            _myDbContext.Add(product);
            _myDbContext.SaveChanges();
            return Redirect("Index");
        }
    }
}

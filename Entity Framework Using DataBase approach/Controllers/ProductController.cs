using Entity_Framework.Data;
using Entity_Framework_Using_DataBase_approach.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Entity_Framework_Using_DataBase_approach.Controllers
{
    public class ProductController : Controller
    {
        private readonly MyDbContext myDbContext;

        public ProductController(MyDbContext myDbContext)
        {
            this.myDbContext = myDbContext;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var products = myDbContext.Products.Include("Category").ToList();
            return View(products);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var product = myDbContext.Products
                             .Include(p => p.Category)
                             .FirstOrDefault(p => p.ProductId == id);
            return View(product);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Categories = myDbContext.Categories.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = myDbContext.Categories.ToList();
                return View(product);
            }

            myDbContext.Products.Add(product);
            myDbContext.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            var product = myDbContext.Products.Find(id);
            ViewBag.CategoryId = new SelectList(myDbContext.Categories, "CategoryId", "CategoryName", product.CategoryId);
            ViewBag.Categories = myDbContext.Categories.ToList();
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            myDbContext.Entry(product).State = EntityState.Modified;
            myDbContext.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Delete(int id)
        {
            var product = myDbContext.Products.Find(id);
            myDbContext.Products.Remove(product);
            myDbContext.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
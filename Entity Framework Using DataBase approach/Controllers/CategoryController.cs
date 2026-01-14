using Entity_Framework.Data;
using Entity_Framework_Using_DataBase_approach.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Entity_Framework_Using_DataBase_approach.Controllers
{
    public class CategoryController : Controller
    {
        private readonly MyDbContext myDbContext;

        public CategoryController(MyDbContext myDbContext)
        {
            this.myDbContext = myDbContext;
        }

        public ActionResult Index()
        {
            return View(myDbContext.Categories.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Category category)
        {
            myDbContext.Categories.Add(category);
            myDbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var category = myDbContext.Categories.Find(id);
            if (category == null) return NotFound();
            return View(category);
        }

      
        [HttpPost]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                myDbContext.Entry(category).State = EntityState.Modified;
                myDbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }
    }
}

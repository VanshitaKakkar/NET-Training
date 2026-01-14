using AutoMapper;
using Entity_Framework.Data;
using Entity_Framework_Using_DataBase_approach.Models;
using Entity_Framework_Using_DataBase_approach.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Entity_Framework_Using_DataBase_approach.Controllers
{
    [Route("products")]
    public class ProductController : Controller
    {
        private readonly MyDbContext myDbContext;
        private readonly IMapper mapper;

        public ProductController(MyDbContext myDbContext, IMapper mapper)
        {
            this.myDbContext = myDbContext;
            this.mapper = mapper;
        }

        // ================= INDEX =================
        [HttpGet("")]
        public IActionResult Index()
        {
            ViewBag.PageTitle = "Product Management";
            ViewBag.InfoText = "Manage products using filters and actions";
            ViewData["TotalProducts"] = myDbContext.Products.Count();

            ViewBag.Categories = myDbContext.Categories.ToList();
            return View();
        }

        // ================= AJAX JSON =================
        [HttpGet("filter-json")]
        public IActionResult FilterJson(int? categoryId, string search, string sortOrder)
        {
            var query = myDbContext.Products
                                   .Include(p => p.Category)
                                   .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(p => p.Name.Contains(search));

            if (categoryId.HasValue)
                query = query.Where(p => p.CategoryId == categoryId);

            query = sortOrder switch
            {
                "price_asc" => query.OrderBy(p => p.Price),
                "price_desc" => query.OrderByDescending(p => p.Price),
                _ => query
            };

            var dtoList = mapper.Map<List<ProductDTO>>(query.ToList());

            return Json(dtoList);
        }

        // ================= CREATE =================
        [HttpGet("create")]
        public IActionResult Create()
        {
            ViewBag.PageTitle = "Add New Product";
            ViewBag.Categories = myDbContext.Categories.ToList();
            return View(new ProductDTO());
        }

        [HttpPost("create")]
        public IActionResult Create(ProductDTO dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = myDbContext.Categories.ToList();
                return View(dto);
            }

            bool exists = myDbContext.Products.Any(p => p.Name == dto.Name);
            if (exists)
            {
                TempData["ErrorMessage"] = "Product already exists";
                return RedirectToAction(nameof(Create));
            }

            var product = mapper.Map<Product>(dto);
            myDbContext.Products.Add(product);
            myDbContext.SaveChanges();

            TempData["SuccessMessage"] = "Product added successfully";
            return RedirectToAction(nameof(Index));
        }

        // ================= EDIT =================
        [HttpGet("edit/{id:int}")]
        public IActionResult Edit(int id)
        {
            var product = myDbContext.Products.Find(id);
            if (product == null)
            {
                TempData["ErrorMessage"] = "Product not found";
                return RedirectToAction(nameof(Index));
            }

            var dto = mapper.Map<ProductDTO>(product);

            ViewBag.PageTitle = "Edit Product";
            ViewBag.Categories = myDbContext.Categories.ToList();
            return View(dto);
        }

        [HttpPost("edit/{id:int}")]
        public IActionResult Edit(ProductDTO dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = myDbContext.Categories.ToList();
                return View(dto);
            }

            var product = mapper.Map<Product>(dto);
            myDbContext.Entry(product).State = EntityState.Modified;
            myDbContext.SaveChanges();

            TempData["SuccessMessage"] = "Product updated successfully";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("delete/{id:int}")]
        public IActionResult Delete(int id)
        {
            var product = myDbContext.Products.Find(id);
            if (product == null)
            {
                TempData["ErrorMessage"] = "Product not found";
                return RedirectToAction(nameof(Index));
            }

            myDbContext.Products.Remove(product);
            myDbContext.SaveChanges();

            TempData["SuccessMessage"] = "Product deleted successfully";
            return RedirectToAction(nameof(Index));
        }
    }
}

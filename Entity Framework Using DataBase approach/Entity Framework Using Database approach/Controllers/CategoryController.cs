using AutoMapper;
using Entity_Framework.Data;
using Entity_Framework_Using_DataBase_approach.Models;
using Entity_Framework_Using_DataBase_approach.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Entity_Framework_Using_DataBase_approach.Controllers
{
    [Route("categories")]
    public class CategoryController : Controller
    {
        private readonly MyDbContext myDbContext;
        private readonly IMapper mapper;

        public CategoryController(MyDbContext myDbContext, IMapper mapper)
        {
            this.myDbContext = myDbContext;
            this.mapper = mapper;
        }

        // GET: /categories
        [HttpGet("")]
        public IActionResult Index()
        {
            ViewBag.PageTitle = "Category Management";
            ViewData["TotalCategories"] = myDbContext.Categories.Count();

            var categories = myDbContext.Categories.ToList();
            var dtoList = mapper.Map<List<CategoryDTO>>(categories);

            return View(dtoList);
        }

        // GET: /categories/create
        [HttpGet("create")]
        public IActionResult Create()
        {
            ViewBag.PageTitle = "Add Category";
            return View();
        }

        // POST: /categories/create
        [HttpPost("create")]
        public IActionResult Create(CategoryDTO dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            bool exists = myDbContext.Categories
                .Any(c => c.CategoryName == dto.CategoryName);

            if (exists)
            {
                TempData["ErrorMessage"] = "Category already exists";
                return RedirectToAction(nameof(Create));
            }

            var category = mapper.Map<Category>(dto);

            myDbContext.Categories.Add(category);
            myDbContext.SaveChanges();

            TempData["SuccessMessage"] = "Category added successfully";
            return RedirectToAction(nameof(Index));
        }

        // GET: /categories/edit/5
        [HttpGet("edit/{id:int}")]
        public IActionResult Edit(int id)
        {
            var category = myDbContext.Categories.Find(id);

            if (category == null)
            {
                TempData["ErrorMessage"] = "Category not found";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.PageTitle = "Edit Category";
            var dto = mapper.Map<CategoryDTO>(category);

            return View(dto);
        }

        // POST: /categories/edit/5
        [HttpPost("edit/{id:int}")]
        public IActionResult Edit(CategoryDTO dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var category = myDbContext.Categories.Find(dto.CategoryId);
            if (category == null)
            {
                TempData["ErrorMessage"] = "Category not found";
                return RedirectToAction(nameof(Index));
            }

            mapper.Map(dto, category);
            myDbContext.SaveChanges();

            TempData["SuccessMessage"] = "Category updated successfully";
            return RedirectToAction(nameof(Index));
        }
    }
}

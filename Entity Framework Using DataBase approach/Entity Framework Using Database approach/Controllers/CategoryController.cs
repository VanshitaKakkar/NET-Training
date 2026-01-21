using Entity_Framework_Using_DataBase_approach.Models.ViewModels;
using Entity_Framework_Using_DataBase_approach.Services.InterFaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
[Route("categories")]
public class CategoryController : Controller
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        ViewBag.PageTitle = "Category Management";
        ViewData["TotalCategories"] = await _categoryService.GetTotalCategoriesAsync();
        var categories = await _categoryService.GetAllCategoriesAsync();
        return View(categories);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("create")]
    public IActionResult Create()
    {
        ViewBag.PageTitle = "Add Category";
        return View();
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("create")]
    public async Task<IActionResult> Create(CategoryDTO dto)
    {
        if (!ModelState.IsValid)
            return View(dto);

        if (await _categoryService.CategoryExistsAsync(dto.CategoryName))
        {
            TempData["ErrorMessage"] = "Category already exists";
            return RedirectToAction(nameof(Create));
        }

        await _categoryService.CreateCategoryAsync(dto);
        TempData["SuccessMessage"] = "Category added successfully";
        return RedirectToAction(nameof(Index));
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("edit/{id:int}")]
    public async Task<IActionResult> Edit(int id)
    {
        var category = await _categoryService.GetCategoryByIdAsync(id);
        if (category == null)
        {
            TempData["ErrorMessage"] = "Category not found";
            return RedirectToAction(nameof(Index));
        }

        ViewBag.PageTitle = "Edit Category";
        return View(category);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("edit/{id:int}")]
    public async Task<IActionResult> Edit(CategoryDTO dto)
    {
        if (!ModelState.IsValid)
            return View(dto);

        bool updated = await _categoryService.UpdateCategoryAsync(dto);
        if (!updated)
        {
            TempData["ErrorMessage"] = "Category not found";
            return RedirectToAction(nameof(Index));
        }

        TempData["SuccessMessage"] = "Category updated successfully";
        return RedirectToAction(nameof(Index));
    }
}

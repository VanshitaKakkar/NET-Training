
using Entity_Framework_Using_DataBase_approach.Models.ViewModels;
using Entity_Framework_Using_DataBase_approach.Services.InterFaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



[Route("products")]
[Authorize]
public class ProductController : Controller
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        ViewBag.PageTitle = "Product Management";
        ViewBag.InfoText = "Manage products using filters and actions";

        ViewData["TotalProducts"] = await _productService.GetTotalProductsAsync();
        ViewBag.Categories = await _productService.GetAllCategoriesAsync();

        return View();
    }

    [HttpGet("filter-json")]
    public async Task<IActionResult> FilterJson(int? categoryId, string search, string sortOrder)
    {
        var products = await _productService.FilterProductsAsync(categoryId, search, sortOrder);
        return Json(products);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> Details(int id)
    {
        var product = await _productService.GetProductByIdAsync(id);
        return View(product);
    }
    [HttpGet("create")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create()
    {
        ViewBag.PageTitle = "Add New Product";
        ViewBag.Categories = await _productService.GetAllCategoriesAsync();
        return View(new ProductDTO());
    }

    [HttpPost("create")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create(ProductDTO dto)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Categories = await _productService.GetAllCategoriesAsync();
            return View(dto);
        }

        if (await _productService.ProductExistsAsync(dto.Name))
        {
            TempData["ErrorMessage"] = "Product already exists";
            return RedirectToAction(nameof(Create));
        }

        await _productService.AddProductAsync(dto);
        TempData["SuccessMessage"] = "Product added successfully";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet("edit/{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(int id)
    {
        var dto = await _productService.GetProductByIdAsync(id);
        if (dto == null)
        {
            TempData["ErrorMessage"] = "Product not found";
            return RedirectToAction(nameof(Index));
        }

        ViewBag.PageTitle = "Edit Product";
        ViewBag.Categories = await _productService.GetAllCategoriesAsync();
        return View(dto);
    }

    [HttpPost("edit/{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(ProductDTO dto)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Categories = await _productService.GetAllCategoriesAsync();
            return View(dto);
        }

        await _productService.UpdateProductAsync(dto);
        TempData["SuccessMessage"] = "Product updated successfully";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet("delete/{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        await _productService.DeleteProductAsync(id);
        TempData["SuccessMessage"] = "Product deleted successfully";
        return RedirectToAction(nameof(Index));
    }
}
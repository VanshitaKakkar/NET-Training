using System.Diagnostics;
using Assignment_3.Filter;
using Assignment_3.Models;
using Microsoft.AspNetCore.Mvc;

namespace Assignment_3.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("about")]
        [ActionLogFilter]
        public IActionResult About()
        {
            return View();
        }
       
    }
}

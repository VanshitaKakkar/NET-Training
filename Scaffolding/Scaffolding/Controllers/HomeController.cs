using System.Diagnostics;
using MVC_Assignment.Filter;
using MVC_Assignment.Models;
using Microsoft.AspNetCore.Mvc;

namespace MVC_Assignment.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
       
    }
}

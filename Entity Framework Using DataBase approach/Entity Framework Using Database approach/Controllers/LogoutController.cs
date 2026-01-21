

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Entity_Framework_Using_DataBase_approach.Controllers
{
    [Route("logout")]
    public class LogoutController : Controller

    {
        [Authorize]
        [HttpGet]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt");
            return RedirectToAction("Index", "Login");
        }
    }
}

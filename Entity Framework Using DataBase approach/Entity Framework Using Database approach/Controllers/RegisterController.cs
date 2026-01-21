using Entity_Framework.Data;
using Entity_Framework_Using_DataBase_approach.Models;
using Entity_Framework_Using_DataBase_approach.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

[Route("register")] 
public class RegisterController : Controller
{
    private readonly MyDbContext _context;

    public RegisterController(MyDbContext context)
    {
        _context = context;
    }

    // GET: /Register
    [HttpGet("")]
    [AllowAnonymous]
    public IActionResult Index()
    {
        return View("Register");
    }

    // POST: /Register
    [HttpPost("")]
    [AllowAnonymous]
    public IActionResult Register(RegisterDTO registerDTO)
    {
        if (!ModelState.IsValid)
            return View("Index", registerDTO);

        bool userExists = _context.Users.Any(u => u.UserName == registerDTO.UserName);
        if (userExists)
        {
            ModelState.AddModelError("", "Username already exists");
            return View("Index", registerDTO);
        }

        var user = new User
        {
            UserName = registerDTO.UserName,
            PasswordHash = HashPassword(registerDTO.Password),
            Role = registerDTO.Role
        };

        _context.Users.Add(user);
        _context.SaveChanges();

      
        return RedirectToAction("Login", "/login");
    }

    private string HashPassword(string password)
    {
        using var sha = SHA256.Create();
        var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(bytes);
    }
}

using Entity_Framework.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Entity_Framework_Using_DataBase_approach.Controllers
{
    [Route("login")]   
    public class LoginController : Controller
    {
        private readonly MyDbContext _context;

        public LoginController(MyDbContext myDbContext)
        {
            _context = myDbContext;
        }

        // GET: /login
        [HttpGet("")]
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View("Login");
        }

        // POST: /login
        [HttpPost("")]
        [AllowAnonymous]
        public IActionResult Index(string username, string password)
        {
            string hashedPassword = HashPassword(password);

            var user = _context.Users
                .FirstOrDefault(u => u.UserName == username && u.PasswordHash == hashedPassword);

            if (user == null)
            {
                ViewBag.Error = "Invalid Username or Password";
                return View("Login");
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("THIS_IS_A_VERY_SECURE_256_BIT_JWT_SECRET_KEY!")
            );

            var token = new JwtSecurityToken(
                issuer: "EcommerceApp",
                audience: "EcommerceUsers",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            Response.Cookies.Append("jwt", tokenString, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict
            });

            return RedirectToAction("Index", "Product");
        }


        private string HashPassword(string password)
        {
            using var sha = System.Security.Cryptography.SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }
}

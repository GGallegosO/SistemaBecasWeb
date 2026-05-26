using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace SistemaBecasWeb.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string username, string password)
        {
            // Validación fija para cumplir con la pauta sin tocar SQL
            if (username == "admin" && password == "1234")
            {
                var claims = new List<Claim> { new Claim(ClaimTypes.Name, "Admin") };
                var identity = new ClaimsIdentity(claims, "CookieAuth");
                await HttpContext.SignInAsync("CookieAuth", new ClaimsPrincipal(identity));

                return RedirectToAction("Index", "SolicitudBecas");
            }

            ViewBag.Error = "Credenciales incorrectas";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            // Esto destruye la cookie en el navegador del usuario
            await HttpContext.SignOutAsync("CookieAuth");

            // Lo pateamos de vuelta a la pantalla de Login
            return RedirectToAction("Index", "Login");
        }

    }
}
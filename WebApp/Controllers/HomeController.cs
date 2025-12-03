using Microsoft.AspNetCore.Mvc;
using WebApp.Filters;

namespace WebApp.Controllers;

[ServiceFilter(typeof(AuthenticationFilter))]
public class HomeController : Controller
{
    public IActionResult Index()
    {
        // Get user info from session
        ViewBag.UserName = HttpContext.Session.GetString("UserName");
        ViewBag.UserEmail = HttpContext.Session.GetString("UserEmail");
        
        return View();
    }
}

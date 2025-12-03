using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Controllers;

public class AuthController : Controller
{
    private readonly ApiService _apiService;

    public AuthController(ApiService apiService)
    {
        _apiService = apiService;
    }

    [HttpGet]
    public IActionResult Login()
    {
        // Check if already logged in
        var token = HttpContext.Session.GetString("JWTToken");
        if (!string.IsNullOrEmpty(token))
        {
            return RedirectToAction("Index", "Home");
        }

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        try
        {
            var loginData = new { Email = model.Email, Password = model.Password };
            var response = await _apiService.PostAsync<LoginResponseDTO>("/api/auth/login", loginData);

            if (response != null && !string.IsNullOrEmpty(response.Token))
            {
                // Store token and user info in session
                HttpContext.Session.SetString("JWTToken", response.Token);
                HttpContext.Session.SetString("UserEmail", response.User.Email);
                HttpContext.Session.SetString("UserName", response.User.FullName);
                HttpContext.Session.SetInt32("UserId", response.User.Id);
                HttpContext.Session.SetInt32("UserRole", response.User.Role);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Email or password is invalid");
                return View(model);
            }
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, "An error occurred during login. Please try again.");
            return View(model);
        }
    }

    [HttpGet]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }
}

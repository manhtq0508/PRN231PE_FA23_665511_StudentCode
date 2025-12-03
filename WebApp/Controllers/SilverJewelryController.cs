using Microsoft.AspNetCore.Mvc;
using WebApp.Filters;
using WebApp.Services;
using System.Text.Json;

namespace WebApp.Controllers;

[ServiceFilter(typeof(AuthenticationFilter))]
public class SilverJewelryController : Controller
{
    private readonly ApiService _apiService;

    public SilverJewelryController(ApiService apiService)
    {
        _apiService = apiService;
    }

    public async Task<IActionResult> Index()
    {
        try
        {
            var token = HttpContext.Session.GetString("JWTToken");
            if (!string.IsNullOrEmpty(token))
            {
                _apiService.SetAuthorizationHeader(token);
            }

            var response = await _apiService.GetAsync<object>("/api/silverjewelry");
            
            ViewBag.Data = response;
            return View();
        }
        catch (Exception ex)
        {
            ViewBag.Error = "Error loading data: " + ex.Message;
            return View();
        }
    }
}

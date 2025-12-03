using Microsoft.AspNetCore.Mvc;
using WebApp.Filters;
using WebApp.Services;
using WebApp.Models;
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

    [RoleAuthorize(1, 4)]
    public async Task<IActionResult> Index(SilverJewelrySearchViewModel search)
    {
        try
        {
            var token = HttpContext.Session.GetString("JWTToken");
            var queryParams = new List<string>();

            if (!string.IsNullOrWhiteSpace(search.Name))
                queryParams.Add($"Name={Uri.EscapeDataString(search.Name)}");
            if (search.MinMetalWeight.HasValue)
                queryParams.Add($"MinMetalWeight={search.MinMetalWeight}");
            if (search.MaxMetalWeight.HasValue)
                queryParams.Add($"MaxMetalWeight={search.MaxMetalWeight}");

            var queryString = queryParams.Count > 0 ? "?" + string.Join("&", queryParams) : "";
            var response = await _apiService.GetAsync<List<SilverJewelryViewModel>>($"/api/silver-jewelries{queryString}");

            if (response != null && response.Count > 0)
            {
                var categories = await _apiService.GetAsync<List<CategoryDTO>>("/api/category");
                if (categories != null)
                {
                    foreach (var item in response)
                    {
                        item.CategoryName = categories.FirstOrDefault(c => c.Id == item.CategoryId)?.Name;
                    }
                }
            }

            ViewBag.SearchModel = search;
            return View(response ?? new List<SilverJewelryViewModel>());
        }
        catch (Exception ex)
        {
            ViewBag.Error = ex.Message;
            return View(new List<SilverJewelryViewModel>());
        }
    }

    [RoleAuthorize(1, 4)]
    public async Task<IActionResult> Details(int id)
    {
        try
        {
            var token = HttpContext.Session.GetString("JWTToken");
            var response = await _apiService.GetAsync<SilverJewelryViewModel>($"/api/silver-jewelries/{id}");
            
            if (response == null)
                return NotFound();

            if (response.CategoryName == null)
            {
                var categories = await _apiService.GetAsync<List<CategoryDTO>>("/api/category");
                response.CategoryName = categories?.FirstOrDefault(c => c.Id == response.CategoryId)?.Name;
            }

            return View(response);
        }
        catch
        {
            return NotFound();
        }
    }

    [RoleAuthorize(1)]
    public async Task<IActionResult> Create()
    {
        await LoadCategories();
        var model = new SilverJewelryViewModel
        {
            CreatedDate = DateOnly.FromDateTime(DateTime.Now)
        };
        return View(model);
    }

    [HttpPost]
    [RoleAuthorize(1)]
    public async Task<IActionResult> Create(SilverJewelryViewModel model)
    {
        if (!ModelState.IsValid)
        {
            await LoadCategories();
            return View(model);
        }

        try
        {
            var token = HttpContext.Session.GetString("JWTToken");
            var createDto = new 
            {
                Name = model.Name,
                Description = model.Description,
                MetalWeight = model.MetalWeight,
                Price = model.Price,
                ProductionYear = model.ProductionYear,
                CategoryId = model.CategoryId
            };
            var response = await _apiService.PostWithTokenAsync("/api/silver-jewelries", createDto, token!);

            if (response.IsSuccessStatusCode)
            {
                TempData["Success"] = "Created successfully";
                return RedirectToAction(nameof(Index));
            }

            var error = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError("", error);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
        }

        await LoadCategories();
        return View(model);
    }

    [RoleAuthorize(1)]
    public async Task<IActionResult> Edit(int id)
    {
        try
        {
            var token = HttpContext.Session.GetString("JWTToken");
            var response = await _apiService.GetAsync<SilverJewelryViewModel>($"/api/silver-jewelries/{id}");
            
            if (response == null)
                return NotFound();

            await LoadCategories();
            return View(response);
        }
        catch
        {
            return NotFound();
        }
    }

    [HttpPost]
    [RoleAuthorize(1)]
    public async Task<IActionResult> Edit(int id, SilverJewelryViewModel model)
    {
        if (id != model.Id)
            return BadRequest();

        if (!ModelState.IsValid)
        {
            await LoadCategories();
            return View(model);
        }

        try
        {
            var token = HttpContext.Session.GetString("JWTToken");
            var updateDto = new 
            {
                Name = model.Name,
                Description = model.Description,
                MetalWeight = model.MetalWeight,
                Price = model.Price,
                ProductionYear = model.ProductionYear,
                CategoryId = model.CategoryId
            };
            var response = await _apiService.PutWithTokenAsync($"/api/silver-jewelries/{id}", updateDto, token!);

            if (response.IsSuccessStatusCode)
            {
                TempData["Success"] = "Updated successfully";
                return RedirectToAction(nameof(Index));
            }

            var error = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError("", error);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
        }

        await LoadCategories();
        return View(model);
    }

    [RoleAuthorize(1)]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var token = HttpContext.Session.GetString("JWTToken");
            var response = await _apiService.GetAsync<SilverJewelryViewModel>($"/api/silver-jewelries/{id}");
            
            if (response == null)
                return NotFound();

            if (response.CategoryName == null)
            {
                var categories = await _apiService.GetAsync<List<CategoryDTO>>("/api/category");
                response.CategoryName = categories?.FirstOrDefault(c => c.Id == response.CategoryId)?.Name;
            }

            return View(response);
        }
        catch
        {
            return NotFound();
        }
    }

    [HttpPost, ActionName("Delete")]
    [RoleAuthorize(1)]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        try
        {
            var token = HttpContext.Session.GetString("JWTToken");
            var response = await _apiService.DeleteWithTokenAsync($"/api/silver-jewelries/{id}", token!);

            if (response.IsSuccessStatusCode)
            {
                TempData["Success"] = "Deleted successfully";
                return RedirectToAction(nameof(Index));
            }

            TempData["Error"] = "Failed to delete";
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
        }

        return RedirectToAction(nameof(Index));
    }

    private async Task LoadCategories()
    {
        try
        {
            var token = HttpContext.Session.GetString("JWTToken");
            var categories = await _apiService.GetAsync<List<CategoryDTO>>("/api/category");
            ViewBag.Categories = categories ?? new List<CategoryDTO>();
        }
        catch
        {
            ViewBag.Categories = new List<CategoryDTO>();
        }
    }
}

using BusinessObject;
using DataAccess.QueryObject;
using DataAccess.Repositories;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DTO.SilverJewelry;

namespace WebAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/silver-jewelries")]
public class SilverJewelryController : ControllerBase
{
    private readonly ISilverJewelryRepository _silverJewelryRepo;

    public SilverJewelryController(ISilverJewelryRepository silverJewelryRepo)
    {
        _silverJewelryRepo = silverJewelryRepo;
    }

    [Authorize(Roles = "Admin,Staff")]
    [HttpGet]
    public async Task<IActionResult> GetSilverJewelries([FromQuery] SilverJewelryQuery query)
    {
        var result = await _silverJewelryRepo.GetSilverJewelriesAsync(query);
        var resultDTO = result.Adapt<List<SilverJewelryDTO>>();

        return Ok(resultDTO);
    }

    [Authorize(Roles = "Admin,Staff")]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetSilverJewelryById(int id)
    {
        var silverJewelry = await _silverJewelryRepo.GetSilverJewelryByIdAsync(id);
        if (silverJewelry == null)
        {
            return NotFound($"Silver jewelry with ID {id} not found.");
        }
        var silverJewelryDTO = silverJewelry.Adapt<SilverJewelryDTO>();
        return Ok(silverJewelryDTO);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> AddSilverJewelry([FromBody] CreateSilverJewelryDTO silverJewelryCreateDTO)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).Distinct().ToList();
            var errorMessage = string.Join("\n", errors);
            return BadRequest(errorMessage);
        }

        var silverJewelry = silverJewelryCreateDTO.Adapt<SilverJewelry>();
        await _silverJewelryRepo.AddSilverJewelryAsync(silverJewelry);

        return CreatedAtAction(nameof(GetSilverJewelryById), new { id = silverJewelry.Id }, null);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateSilverJewelry(int id, [FromBody] PutSilverJewelryDTO silverJewelryUpdateDTO)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).Distinct().ToList();
            var errorMessage = string.Join("\n", errors);
            return BadRequest(errorMessage);
        }
        var existingSilverJewelry = await _silverJewelryRepo.GetSilverJewelryByIdAsync(id);
        if (existingSilverJewelry == null)
        {
            return NotFound($"Silver jewelry with ID {id} not found.");
        }
        var updatedSilverJewelry = silverJewelryUpdateDTO.Adapt<SilverJewelry>();
        updatedSilverJewelry.Id = id;
        await _silverJewelryRepo.UpdateSilverJewelryAsync(updatedSilverJewelry);
        return NoContent();
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteSilverJewelry(int id)
    {
        var existingSilverJewelry = await _silverJewelryRepo.GetSilverJewelryByIdAsync(id);
        if (existingSilverJewelry == null)
        {
            return NotFound($"Silver jewelry with ID {id} not found.");
        }
        await _silverJewelryRepo.DeleteSilverJewelryAsync(id);
        return NoContent();
    }
}

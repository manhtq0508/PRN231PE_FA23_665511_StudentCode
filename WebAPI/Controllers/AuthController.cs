using BusinessObject;
using DataAccess.Repositories;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPI.DTO.Auth;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IBranchAccountRepository _branchAccountRepo;
    private readonly IConfiguration _config;

    public AuthController(IConfiguration config, IBranchAccountRepository branchAccountRepo)
    {
        _branchAccountRepo = branchAccountRepo;
        _config = config;
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] AuthDTO authDto)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).Distinct().ToList();
            var errorMessage = string.Join("\n", errors);
            return BadRequest(errorMessage);
        }

        var account = await _branchAccountRepo.AuthenticateAsync(authDto.Email, authDto.Password);
        var accountDTO = account.Adapt<BranchAccountDTO>();

        if (account != null)
        {
            var token = GenerateJwtToken(account);
            var response = new LoginResponseDTO
            {
                Token = token,
                User = accountDTO
            };

            return Ok(response);
        }

        return Unauthorized("Email or password is invalid");
    }

    private string GenerateJwtToken(BranchAccount account)
    {
        var jwtSettings = _config.GetSection("Jwt");
        var strKey = jwtSettings["Key"];
        if (string.IsNullOrWhiteSpace(strKey))
        {
            throw new Exception("Missing JWT key");
        }

        var key = Encoding.UTF8.GetBytes(strKey);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
            [
                new Claim(ClaimTypes.NameIdentifier, account.Id.ToString()),
                new Claim(ClaimTypes.Email, account.Email),
                new Claim(ClaimTypes.Role, account.Role.ToString())
            ]),
            Expires = DateTime.UtcNow.AddHours(1),
            Issuer = jwtSettings["Issuer"],
            Audience = jwtSettings["Audience"],
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}

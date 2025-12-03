using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTO.Auth;

public class AuthDTO
{
    [Required(AllowEmptyStrings = false, ErrorMessage = "Email is required")]
    public string Email { get; set; } = null!;

    [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required")]
    public string Password { get; set; } = null!;
}

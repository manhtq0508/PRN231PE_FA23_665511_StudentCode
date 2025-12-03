namespace WebAPI.DTO.Auth;

public class LoginResponseDTO
{
    public string Token { get; set; } = null!;
    public BranchAccountDTO User { get; set; } = null!;
}

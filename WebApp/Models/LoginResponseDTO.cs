namespace WebApp.Models;

public class LoginResponseDTO
{
    public string Token { get; set; } = null!;
    public BranchAccountDTO User { get; set; } = null!;
}

public class BranchAccountDTO
{
    public int Id { get; set; }
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public int Role { get; set; }
}

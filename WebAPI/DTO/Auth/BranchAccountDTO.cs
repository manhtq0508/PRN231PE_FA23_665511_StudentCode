using BusinessObject;

namespace WebAPI.DTO.Auth;

public class BranchAccountDTO
{
    public int Id { get; set; }
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public AccountRole Role { get; set; }
}

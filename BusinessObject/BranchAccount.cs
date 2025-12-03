namespace BusinessObject;

public enum AccountRole
{
    Admin = 1,
    Member = 2,
    Manager = 3,
    Staff = 4
}

public class BranchAccount
{
    public int Id { get; set; }
    public string Password { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public AccountRole Role { get; set; }
}

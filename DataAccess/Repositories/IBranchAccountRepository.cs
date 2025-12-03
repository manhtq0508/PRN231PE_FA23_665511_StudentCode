using BusinessObject;

namespace DataAccess.Repositories;

public interface IBranchAccountRepository
{
    Task<BranchAccount?> AuthenticateAsync(string email, string password);
}

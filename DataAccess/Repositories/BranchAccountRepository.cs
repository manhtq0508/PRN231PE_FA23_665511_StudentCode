using BusinessObject;
using DataAccess.DAO;

namespace DataAccess.Repositories;

public class BranchAccountRepository : IBranchAccountRepository
{
    public Task<BranchAccount?> AuthenticateAsync(string email, string password) => BranchAccountDAO.AuthenticateAsync(email, password);
}

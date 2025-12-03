using BusinessObject;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DAO;

public static class BranchAccountDAO
{
    public static async Task<BranchAccount?> AuthenticateAsync(string email, string password)
    {
        email = email.Trim();
        password = password.Trim();

        using var context = new AppDbContext();

        return await context.BranchAccounts
                      .FirstOrDefaultAsync(account => account.Email == email && account.Password == password);
    }
}

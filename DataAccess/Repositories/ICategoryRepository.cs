using BusinessObject;

namespace DataAccess.Repositories;

public interface ICategoryRepository
{
    Task<List<Category>> GetCategoriesAsync();
}

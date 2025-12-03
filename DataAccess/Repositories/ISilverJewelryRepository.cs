using BusinessObject;
using DataAccess.QueryObject;

namespace DataAccess.Repositories;

public interface ISilverJewelryRepository
{
    Task<List<SilverJewelry>> GetSilverJewelriesAsync(SilverJewelryQuery query);
    Task<SilverJewelry?> GetSilverJewelryByIdAsync(int id);
    Task AddSilverJewelryAsync(SilverJewelry silverJewelry);
    Task UpdateSilverJewelryAsync(SilverJewelry silverJewelry);
    Task DeleteSilverJewelryAsync(int id);
}

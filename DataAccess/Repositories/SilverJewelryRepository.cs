using BusinessObject;
using DataAccess.DAO;
using DataAccess.QueryObject;

namespace DataAccess.Repositories;

public class SilverJewelryRepository : ISilverJewelryRepository
{
    public Task<List<SilverJewelry>> GetSilverJewelriesAsync(SilverJewelryQuery query) => SilverJewelryDAO.GetSilverJewelriesAsync(query);
    public Task<SilverJewelry?> GetSilverJewelryByIdAsync(int id) => SilverJewelryDAO.GetSilverJewelryByIdAsync(id);
    public Task AddSilverJewelryAsync(SilverJewelry silverJewelry) => SilverJewelryDAO.AddSilverJewelryAsync(silverJewelry);
    public Task UpdateSilverJewelryAsync(SilverJewelry silverJewelry) => SilverJewelryDAO.UpdateSilverJewelryAsync(silverJewelry);
    public Task DeleteSilverJewelryAsync(int id) => SilverJewelryDAO.DeleteSilverJewelryAsync(id);
}

using BusinessObject;
using DataAccess.QueryObject;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DAO;

public static class SilverJewelryDAO
{
    public static async Task<List<SilverJewelry>> GetSilverJewelriesAsync(SilverJewelryQuery query)
    {
        using var context = new AppDbContext();

        var queryable = context.SilverJewelries
            .Include(sj => sj.Category)
            .AsNoTrackingWithIdentityResolution()
            .AsQueryable();

        if (!string.IsNullOrEmpty(query.Name))
        {
            queryable = queryable.Where(sj => sj.Name.Contains(query.Name));
        }

        if (query.MinMetalWeight.HasValue)
        {
            queryable = queryable.Where(sj => sj.MetalWeight >= query.MinMetalWeight.Value);
        }

        if (query.MaxMetalWeight.HasValue)
        {
            queryable = queryable.Where(sj => sj.MetalWeight <= query.MaxMetalWeight.Value);
        }

        var result = await queryable.ToListAsync();

        return result;
    }

    public static async Task<SilverJewelry?> GetSilverJewelryByIdAsync(int id)
    {
        using var context = new AppDbContext();
        var result = await context.SilverJewelries
            .Include(sj => sj.Category)
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(sj => sj.Id == id);
        return result;
    }

    public static async Task AddSilverJewelryAsync(SilverJewelry silverJewelry)
    {
        using var context = new AppDbContext();
        context.SilverJewelries.Add(silverJewelry);
        await context.SaveChangesAsync();
    }

    public static async Task UpdateSilverJewelryAsync(SilverJewelry silverJewelry)
    {
        using var context = new AppDbContext();
        var existingSilverJewelry = await context.SilverJewelries.FindAsync(silverJewelry.Id);
        if (existingSilverJewelry == null)
        {
            throw new InvalidOperationException("Silver jewelry not found.");
        }

        context.Entry(existingSilverJewelry).CurrentValues.SetValues(silverJewelry);
        await context.SaveChangesAsync();
    }

    public static async Task DeleteSilverJewelryAsync(int id)
    {
        using var context = new AppDbContext();
        var silverJewelry = await context.SilverJewelries.FindAsync(id);
        if (silverJewelry != null)
        {
            context.SilverJewelries.Remove(silverJewelry);
            await context.SaveChangesAsync();
        }
    }
}

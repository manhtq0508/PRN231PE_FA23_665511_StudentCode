using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BusinessObject;

public class AppDbContext : DbContext
{
    public DbSet<BranchAccount> BranchAccounts { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<SilverJewelry> SilverJewelries { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionString = config.GetConnectionString("DB");
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException("Connection string 'DB' not found.");
        }

        optionsBuilder.UseSqlServer(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BranchAccount>().HasData(
            new BranchAccount { Id = 1, Password = "admin", Email = "admin@gmail.com", FullName = "Admin", Role = AccountRole.Admin },
            new BranchAccount { Id = 2, Password = "member", Email = "member@gmail.com", FullName = "Member", Role = AccountRole.Member },
            new BranchAccount { Id = 3, Password = "manager", Email = "manager@gmail.com", FullName = "Manager", Role = AccountRole.Manager },
            new BranchAccount { Id = 4, Password = "staff", Email = "staff@gmail.com", FullName = "Staff", Role = AccountRole.Staff }
        );

        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Rings", Description = "Description 1", FromCountry = "USA" },
            new Category { Id = 2, Name = "Necklaces", Description = "Description 2", FromCountry = "Vietnam" },
            new Category { Id = 3, Name = "Bracelets", Description = "Description 3", FromCountry = "Japan" },
            new Category { Id = 4, Name = "Earrings", Description = "Description 4", FromCountry = "China" }
        );

        modelBuilder.Entity<SilverJewelry>().HasData(
            new SilverJewelry { Id = 1, Name = "Silver Jewelry 1", Description = "Description 1", MetalWeight = 0.1f, Price = 50.0m, ProductionYear = 1999, CreatedDate = new DateOnly(1999, 1, 1), CategoryId = 1 },
            new SilverJewelry { Id = 2, Name = "Silver Jewelry 2", Description = "Description 2", MetalWeight = 1.1f, Price = 10.0m, ProductionYear = 2000, CreatedDate = new DateOnly(2000, 2, 1), CategoryId = 2 },
            new SilverJewelry { Id = 3, Name = "Silver Jewelry 3", Description = "Description 3", MetalWeight = 2.1f, Price = 24.5m, ProductionYear = 2021, CreatedDate = new DateOnly(2021, 12, 12), CategoryId = 3 },
            new SilverJewelry { Id = 4, Name = "Silver Jewelry 4", Description = "Description 4", MetalWeight = 3.1f, Price = 54.0m, ProductionYear = 1997, CreatedDate = new DateOnly(1997, 1, 1), CategoryId = 4 }
        );
    }
}

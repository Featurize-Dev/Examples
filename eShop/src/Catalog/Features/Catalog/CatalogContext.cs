using Catalog.Features.Catalog.Entities;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Features.Catalog;

public class CatalogContext(DbContextOptions<CatalogContext> options)
    : DbContext(options)
{
    public DbSet<CatalogItem> CatalogItems { get;set; }
    public DbSet<CatalogBrand> CatalogBrand {  get;set; }
    public DbSet<CatalogType> CatalogTypes { get;set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        //builder.HasPostgresExtension("vector");
        
        builder.ApplyConfiguration(new CatalogBrandEntityTypeConfiguration());
        builder.ApplyConfiguration(new CatalogTypeEntityTypeConfiguration());
        builder.ApplyConfiguration(new CatalogItemEntityTypeConfiguration());

        
    }
}

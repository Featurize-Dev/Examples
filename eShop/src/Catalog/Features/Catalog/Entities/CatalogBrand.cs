using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Features.Catalog.Entities;

public class CatalogBrand
{
    public int Id { get; set; }
    public string Brand { get; set; }
}

internal sealed class CatalogBrandEntityTypeConfiguration : IEntityTypeConfiguration<CatalogBrand>
{
    public void Configure(EntityTypeBuilder<CatalogBrand> builder)
    {
        builder.ToTable("CatalogBrand");

        builder.Property(cb => cb.Brand)
            .HasMaxLength(100);
    }
}
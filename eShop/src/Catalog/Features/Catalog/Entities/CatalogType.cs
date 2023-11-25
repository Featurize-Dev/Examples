using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Catalog.Features.Catalog.Entities;

public class CatalogType
{
    public int Id { get; set; }

    [Required]
    public string Type { get; set; }
}


internal sealed class CatalogTypeEntityTypeConfiguration
    : IEntityTypeConfiguration<CatalogType>
{
    public void Configure(EntityTypeBuilder<CatalogType> builder)
    {
        builder.ToTable("CatalogType");

        builder.Property(cb => cb.Type)
            .HasMaxLength(100);
    }
}
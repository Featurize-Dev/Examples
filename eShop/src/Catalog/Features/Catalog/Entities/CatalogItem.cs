using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Pgvector;

namespace Catalog.Features.Catalog.Entities;

public class CatalogItem
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    public string Description { get; set; }
    public decimal Price { get; set; }
    public string PictureFilename { get; set; }
    public string PictureUri { get; set; }

    public int CatalogTypeId { get; set; }
    public CatalogType CatalogType { get; set; }

    public int CatalogBrandId { get;set; }
    public CatalogBrand CatalogBrand { get; set; }

    //[JsonIgnore]
    //public Vector Embedding { get; set; }
    
    
    
}

internal sealed class CatalogItemEntityTypeConfiguration
    : IEntityTypeConfiguration<CatalogItem>
{
    public void Configure(EntityTypeBuilder<CatalogItem> builder)
    {
        builder.ToTable("Catalog");

        builder.Property(ci => ci.Name)
            .HasMaxLength(50);

        builder.Ignore(ci => ci.PictureUri);

        //builder.Property(ci => ci.Embedding)
        //    .HasColumnType("vector(1536)");

        builder.HasOne(ci => ci.CatalogBrand)
            .WithMany();

        builder.HasOne(ci => ci.CatalogType)
            .WithMany();

        builder.HasIndex(ci => ci.Name);
    }
}

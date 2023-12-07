using Catalog.Features.Catalog;
using Catalog.Features.Catalog.Commands;
using Catalog.Features.Catalog.Queries;
using Featurize.AspNetCore;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Features;

public class CatalogFeature : IWebApplicationFeature
{
    public void Configure(IServiceCollection services)
    {
        var con = "Host=nas.home.lab;Database=CatalogDB;Username=postgresUser;Password=postgresPW";

        services.AddDbContext<CatalogContext>(options =>
        {
            options.UseNpgsql(con, config =>
            {
                config.UseVector();
            });
        });

        services.AddProblemDetails();

        services.AddOptions<CatalogOptions>()
            .BindConfiguration(nameof(CatalogOptions));
    }

    public void Use(WebApplication app)
    {
        var group = app.MapGroup("/api/v1/catalog")
           .WithTags("Catalog API");
    
        group.MapGetAllItems();
        group.MapGetItemsByIds();
        group.MapGetItemById();
        group.MapGetItemsByName();
        group.MapGetItemPictureById();

        group.MapCreateItem();
        group.MapDeleteItemById();
        group.MapUpdateItem();
    }
}

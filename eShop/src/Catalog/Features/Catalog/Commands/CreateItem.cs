using Catalog.Features.Catalog.Entities;
using Catalog.Features.Catalog.Queries;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Catalog.Features.Catalog.Commands;

public static class CreateItem
{
    public static void MapCreateItem(this IEndpointRouteBuilder builder)
    {
        builder.MapPost("/items", Handle);
    }

    public static async Task<Created<CatalogItem>> Handle(
        [AsParameters] CatalogServices services,
        CreateItemRequest request)
    {
        var item = new CatalogItem
        {
            CatalogBrandId = request.BrandId,
            CatalogTypeId = request.TypeId,
            Description = request.Description,
            Name = request.Name,
            PictureFilename = request.PictureFilename,
            Price = request.Price
        };

        services.Context.CatalogItems.Add(item);
        await services.Context.SaveChangesAsync();

        return TypedResults.Created($"/api/v1/catalog/items/{item.Id}", item);
    }
}

public record CreateItemRequest(string Name, string Description, int BrandId, int TypeId, string PictureFilename, decimal Price);

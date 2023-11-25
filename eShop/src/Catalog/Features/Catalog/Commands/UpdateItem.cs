using Catalog.Features.Catalog.Entities;
using Catalog.Features.Catalog.Queries;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Features.Catalog.Commands;

public static class UpdateItem
{
    public static void MapUpdateItem(this IEndpointRouteBuilder builder)
    {
        builder.MapPut("/items", Handle);
    }

    public static async Task<Results<CreatedAtRoute, NotFound<string>>> Handle(
        [AsParameters] CatalogServices services,
        UpdateProductRequest productToUpdate)
    {
        var catalogItem = await services.Context.CatalogItems.SingleOrDefaultAsync(x => x.Id == productToUpdate.Id);

        if (catalogItem == null)
        {
            return TypedResults.NotFound($"Item with id {productToUpdate.Id} not found.");
        }

        var catalogEntry = services.Context.Entry(catalogItem);
        catalogEntry.CurrentValues.SetValues(productToUpdate);

        await services.Context.SaveChangesAsync();

        return TypedResults.CreatedAtRoute(nameof(GetItemById), catalogItem.Id);
    }
}

public record UpdateProductRequest(int Id, string Name, string Description);
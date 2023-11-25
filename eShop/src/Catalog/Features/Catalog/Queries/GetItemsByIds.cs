using Catalog.Features.Catalog.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Features.Catalog.Queries;

public static class GetItemsByIds
{
    public static void MapGetItemsByIds(this IEndpointRouteBuilder builder)
    {
        builder.MapGet(Route, Handle);
    }

    public static string Route => "/items/by";

    public static async Task<Ok<List<CatalogItem>>> Handle(
        [AsParameters] CatalogServices services,
        int[] ids)
    {
        var items = await services.Context.CatalogItems.Where(item => ids.Contains(item.Id)).ToListAsync();

        return TypedResults.Ok(items);
    }
}

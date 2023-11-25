using Catalog.Features.Catalog.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Routing.Template;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Features.Catalog.Queries;

public static class GetItemsByName
{
    public static void MapGetItemsByName(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("/items/by/{name:minlength(1)}", Handle);
    }

    public static async Task<Ok<PaginatedItems<CatalogItem>>> Handle(
        [AsParameters] PaginationRequest request,
        [AsParameters] CatalogServices services,
        string name)
    {
        var totalItem = await services.Context.CatalogItems
            .Where(x => x.Name.StartsWith(name))
            .LongCountAsync();

        var itemsOnPage = await services.Context.CatalogItems
            .Where (x => x.Name.StartsWith(name))
            .Skip(request.Skip)
            .Take(request.PageSize)
            .ToListAsync();

        return TypedResults.Ok(new PaginatedItems<CatalogItem>(request.PageIndex, request.PageSize, totalItem, itemsOnPage));
    }
}

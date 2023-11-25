using Catalog.Features.Catalog.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Catalog.Features.Catalog.Queries;

public static class GetAllItems
{
    public static void MapGetAllItems(this IEndpointRouteBuilder builder)
    {
        builder.MapGet(Route, Handle);
    }

    public static string Route => "/items";

    public static async Task<Results<Ok<PaginatedItems<CatalogItem>>, BadRequest<string>>> Handle(
        [AsParameters] PaginationRequest request,
        [AsParameters] CatalogServices services
        )
    {
        var totalItems = await services.Context.CatalogItems.LongCountAsync();

        var itemsOnPage = await services.Context.CatalogItems
            .OrderBy(c => c.Name)
            .Skip(request.Skip)
            .Take(request.PageSize)
            .ToListAsync();

        return TypedResults.Ok(new PaginatedItems<CatalogItem>(request.PageIndex, request.PageSize, totalItems, itemsOnPage));
    }
}

public record PaginationRequest(int PageSize, int PageIndex) {
    public int Skip => PageSize * (PageIndex - 1);
};

public record PaginatedItems<TEntity>(int pageIndex, int pageSize, long count, IEnumerable<TEntity> data)
    where TEntity : class;
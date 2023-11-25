using Catalog.Features.Catalog.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Features.Catalog.Queries;

public static class GetItemById
{
    public static void MapGetItemById(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("/items/{id:int}", Handle)
            .WithName(nameof(GetItemById));
    }

    public static async Task<Results<Ok<CatalogItem>, NotFound, BadRequest<string>>> Handle(
        [AsParameters] CatalogServices services,
        int id)
    {
        if(id <= 0)
        {
            return TypedResults.BadRequest("Id is not valid.");
        }

        var item = await services.Context.CatalogItems.Include(ci => ci.CatalogBrand).SingleOrDefaultAsync(ci => ci.Id == id);

        if(item == null)
        {
            return TypedResults.NotFound();
        }

        return TypedResults.Ok(item);
    }
}

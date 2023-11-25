using Microsoft.AspNetCore.Http.HttpResults;

namespace Catalog.Features.Catalog.Commands;

public static class DeleteItemById
{
    public static void MapDeleteItemById(this IEndpointRouteBuilder builder)
    {
        builder.MapDelete("/items/{id:int}", Handle);
    }

    public static async Task<Results<NoContent, NotFound>> Handle(
        [AsParameters] CatalogServices services,
        int id)
    {
        var item = await services.Context.CatalogItems.FindAsync(id);

        if(item is null)
        {
            return TypedResults.NotFound();
        }

        services.Context.CatalogItems.Remove(item);
        await services.Context.SaveChangesAsync();

        return TypedResults.NoContent();
    }
}

using Microsoft.AspNetCore.Http.HttpResults;

namespace Catalog.Features.Catalog.Queries;

public static class GetItemPictureById
{
    public static void MapGetItemPictureById(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("/items/{id:int}/pic", Handle);
    }

    public static async Task<Results<NotFound, PhysicalFileHttpResult>> Handle(
        [AsParameters] CatalogServices services,
        int id)
    {
        var item = await services.Context.CatalogItems.FindAsync(id);
        if (item == null)
        {
            return TypedResults.NotFound();
        }

        var path = services.GetFullPicturePath(item.PictureFilename);
        var mimetype = services.GetImageMimeTypeFromImageFileExtension(path);

        return TypedResults.PhysicalFile(path, mimetype);
    }
}

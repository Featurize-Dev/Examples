using WebApp.Features.Catalog.Entities;

namespace WebApp.Features.Catalog;

public class CatalogService(HttpClient httpClient)
{
    private readonly string _baseUrl = "api/v1/catalog";

    public Task<CatalogItem?> GetCatalogItem(int id)
    {
        return httpClient.GetFromJsonAsync<CatalogItem?>($"{_baseUrl}/{id}");
    }

    public string Url(CatalogItem item)
        => $"item/{item.Id}";

    public async Task<CatalogResult> GetCatalogItems(int pageIndex, int pageSize, int? brandId, int? typeId)
    {
        return new CatalogResult(pageIndex, pageSize, 0, [
            new CatalogItem()
            {
                Id = 1,
                Name = "Test",
                PictureUri = "",
                Price = 1,
            },
            new CatalogItem()
            {
                Id = 1,
                Name = "Test",
                PictureUri = "",
                Price = 1,
            },
            new CatalogItem()
            {
                Id = 1,
                Name = "Test",
                PictureUri = "",
                Price = 1,
            },
            new CatalogItem()
            {
                Id = 1,
                Name = "Test",
                PictureUri = "",
                Price = 1,
            },
            new CatalogItem()
            {
                Id = 1,
                Name = "Test",
                PictureUri = "",
                Price = 1,
            },
            new CatalogItem()
            {
                Id = 1,
                Name = "Test",
                PictureUri = "",
                Price = 1,
            },
            new CatalogItem()
            {
                Id = 1,
                Name = "Test",
                PictureUri = "",
                Price = 1,
            }
            ]);
    }
}

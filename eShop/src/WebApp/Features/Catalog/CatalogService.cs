using WebApp.Features.Catalog.Entities;

namespace WebApp.Features.Catalog;

public class CatalogService(HttpClient httpClient)
{
    private readonly string _baseUrl = "api/v1/catalog/items";

    public async Task<CatalogItem?> GetCatalogItem(int id)
    {
        var result = await httpClient.GetFromJsonAsync<CatalogItem?>($"{_baseUrl}/{id}");
        return result;
    }
     
    public string Url(CatalogItem item)
        => $"item/{item.Id}";

    public async Task<CatalogResult> GetCatalogItems(int pageIndex, int pageSize, int? brandId, int? typeId)
    {
        return new CatalogResult(pageIndex, pageSize, 0, [
            new CatalogItem()
            {
                Id = 1,
                Name = "Test 1",
                PictureFilename = "",
                Price = 1,
            },
            new CatalogItem()
            {
                Id = 2,
                Name = "Test 2",
                PictureFilename = "",
                Price = 1,
            },
            new CatalogItem()
            {
                Id = 3,
                Name = "Test 3",
                PictureFilename = "",
                Price = 1,
            },
            new CatalogItem()
            {
                Id = 4,
                Name = "Test 4",
                PictureFilename = "",
                Price = 1,
            },
            new CatalogItem()
            {
                Id = 5,
                Name = "Test 5",
                PictureFilename = "",
                Price = 1,
            },
            new CatalogItem()
            {
                Id = 6,
                Name = "Test 6",
                PictureFilename = "",
                Price = 1,
            },
            new CatalogItem()
            {
                Id = 7,
                Name = "Test 7",
                PictureFilename = "",
                Price = 1,
            }
            ]);
    }
}

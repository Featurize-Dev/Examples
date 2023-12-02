using WebApp.Features.Catalog.Entities;

namespace WebApp.Features.Catalog;

public class CatalogService(HttpClient httpClient)
{
    private readonly string _baseUrl = "api/v1/catalog";

    public Task<CatalogItem?> GetCatalogItem(int id)
    {
        return httpClient.GetFromJsonAsync<CatalogItem?>($"{_baseUrl}/{id}");
    }
}

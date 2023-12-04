namespace WebApp.Features.Catalog.Entities;

public class CatalogItem
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string PictureUri { get; set; }
    public int Price { get; set; }
}

public record CatalogResult(int PageIndex, int PageSize, int Count, List<CatalogItem> Data);

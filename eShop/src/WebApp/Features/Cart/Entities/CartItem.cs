namespace WebApp.Features.Cart.Entities;

public class CartItem
{
    public required Guid Id { get; set; }
    public int CatalogItemId { get; set; }
    public required string ProductName { get; set; }
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public required string PictureUrl { get; set; }
    public int ProductId { get; internal set; }
}

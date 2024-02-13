namespace BasketApi.Cart.Entities;

public record CustomerBasket
{
    public string UserId { get; set; } = "";
    public List<BasketItem> Items { get; set; } = [];
}

public record BasketItem
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}

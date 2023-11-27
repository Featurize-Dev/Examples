using Ordering.Features.Order.ValueObjects;

namespace Ordering.Features.Order.Entities;

public record OrderItem(ProductId ProductId, string ProductName, decimal UnitPrice, decimal Discount, string PictureUrl, int Units = 1);
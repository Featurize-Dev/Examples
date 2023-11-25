using CommonFeatures.Storage;
using Microsoft.EntityFrameworkCore;
using Ordering.Features.Order.ValueObjects;

namespace Ordering.Features.Order;

public class OrderContext(DbContextOptions<OrderContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AggregateTypeConfiguration<OrderAggregate, OrderId>());
    }
}

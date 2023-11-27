using CommonFeatures.Storage;
using Microsoft.EntityFrameworkCore;
using Ordering.Features.Buyer.ValueObjects;

namespace Ordering.Features.Buyer;

public class BuyerContext(DbContextOptions<BuyerContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AggregateTypeConfiguration<BuyerAggregate, BuyerId>());
    }
}

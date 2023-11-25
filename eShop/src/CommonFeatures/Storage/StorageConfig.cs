using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Featurize.ValueObjects.Interfaces;
using Featurize.DomainModel;

namespace CommonFeatures.Storage;
public sealed class AggregateTypeConfiguration<TAggregate, TId> 
    : IEntityTypeConfiguration<PersistendEvent<TId>>
    where TAggregate : AggregateRoot<TAggregate, TId>
    where TId : struct, IValueObject<TId>, IEquatable<TId>
{
    public void Configure(EntityTypeBuilder<PersistendEvent<TId>> builder)
    {
        var aggregateName = typeof(TAggregate).Name.Replace("Aggregate", string.Empty);
        builder.ToTable($"{aggregateName}_Events");

        builder.Property(o => o.AggregateId)
            .HasConversion(new ValueConverter<TId>());
            
        builder.HasIndex(p => new { p.AggregateId, p.Version })
            .IsUnique();

        builder.HasIndex(p => p.AggregateId)
            .IsUnique(false);
    }
}

public class ValueConverter<TId> : ValueConverter<TId, string>
    where TId : IValueObject<TId>
{
    public ValueConverter()
        : base(dossierId => dossierId.ToString()!, value => ValueConverter<TId>.Parse(value))
    {
    }

    public static TId Parse(string value)
    {
        return TId.Parse(value);
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.OrderAgg.ValueObjects;

namespace Shop.Infrastructure.Persistent.Ef.SiteEntities;

public class ShippingMethodeConfiguration:IEntityTypeConfiguration<OrderShippingMethod>
{
    public void Configure(EntityTypeBuilder<OrderShippingMethod> builder)
    {
        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(120);
    }
}
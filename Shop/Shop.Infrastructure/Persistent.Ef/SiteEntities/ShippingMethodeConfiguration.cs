using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.SiteEntities;

namespace Shop.Infrastructure.Persistent.Ef.SiteEntities;

public class ShippingMethodeConfiguration:IEntityTypeConfiguration<ShippingMethod>
{
    

    public void Configure(EntityTypeBuilder<ShippingMethod> builder)
    {
        builder.Property(b => b.Title)
            .HasMaxLength(120).IsRequired();
    }
}
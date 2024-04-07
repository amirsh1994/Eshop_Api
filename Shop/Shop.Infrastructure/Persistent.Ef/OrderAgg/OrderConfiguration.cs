using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.OrderAgg;

namespace Shop.Infrastructure.Persistent.Ef.OrderAgg;

internal class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders", "order");

        builder.OwnsOne(x => x.Discount, option =>
        {
            option.Property(x => x.DiscountTitle)
                .HasMaxLength(50);
        });

        builder.OwnsOne(x => x.Methode, option =>
        {
            option.Property(x => x.ShippingType)
                .HasMaxLength(50);
        });

        builder.OwnsMany(x => x.Items, option =>
        {
            option.ToTable("Items", "order");
        });

        builder.OwnsOne(x => x.Address, option =>
        {
            option.ToTable("Addresses", "order");

            option.HasKey(x => x.Id);

            option.Property(x => x.City)
                .HasMaxLength(50)
                .IsRequired();

            option.Property(x => x.Family)
                .HasMaxLength(100)
                .IsRequired();

            option.Property(x => x.PhoneNumber)
                .HasMaxLength(11)
                .IsRequired();

            option.Property(x => x.Name)
                .HasMaxLength(100)
                .IsRequired();

            option.Property(x => x.NationalCode)
                .HasMaxLength(11)
                .IsRequired();

            option.Property(x => x.PostalCode)
                .HasMaxLength(40)
                .IsRequired();





        });


    }
}
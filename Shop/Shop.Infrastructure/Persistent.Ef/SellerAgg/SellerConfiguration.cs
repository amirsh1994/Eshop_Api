using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.SellerAgg;

namespace Shop.Infrastructure.Persistent.Ef.SellerAgg;

public class SellerConfiguration:IEntityTypeConfiguration<Seller>
{
    public void Configure(EntityTypeBuilder<Seller> builder)
    {
        builder.ToTable("Sellers", "seller");

        builder.HasIndex(x => x.NationalCode);

        builder.Property(x => x.NationalCode)
            .IsRequired();

        builder.Property(x => x.ShopName)
            .IsRequired();

        builder.OwnsMany(x => x.Inventories, option =>
        {
            option.ToTable("Inventories", "seller");
            option.HasKey(x => x.Id);
            option.HasIndex(x => x.ProductId);
            option.HasIndex(x => x.SellerId);


        });
    }
}
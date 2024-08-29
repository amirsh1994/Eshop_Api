using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.ProductAgg;

namespace Shop.Infrastructure.Persistent.Ef.ProductAgg;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("products", "product");

        builder.HasIndex(x => x.Slug).IsUnique();

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(2000);

        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(40000);

        builder.Property(x => x.ImageName)
            .IsRequired()
            .HasMaxLength(110);

        builder.Property(x => x.Slug)
            .IsRequired()
            .IsUnicode(false);

        builder.OwnsOne(x => x.SeoData, option =>
        {
            option.Property(x => x.MetaDescription)
                .HasMaxLength(500)
                .HasColumnName("MetaDescription");

            option.Property(x => x.MetaTitle)
                .HasMaxLength(500)
                .HasColumnName("MetaTitle");

            option.Property(x => x.MetaKeyWords)
                .HasMaxLength(500)
                .HasColumnName("MetaKeyWords");

            option.Property(x => x.IndexPage)
                .HasColumnName("IndexPage");

            option.Property(x => x.Canonical)
                .HasMaxLength(500)
                .HasColumnName("Canonical");

            option.Property(x => x.Schema)
                .HasColumnName("Schema");
        });

        builder.OwnsMany(x => x.Images, option =>
        {
            option.ToTable("Images", "product");
            option.Property(x => x.ImageName)
                .IsRequired()
                .HasMaxLength(100);

        });

        builder.OwnsMany(x => x.Specifications, option =>
        {
            option.ToTable("Specifications", "product");

            option.Property(x => x.Key)
                .IsRequired()
                .HasMaxLength(50);

            option.Property(x => x.Value)
                .IsRequired()
                .HasMaxLength(100);
        });
    }
}
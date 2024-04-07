using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.SiteEntities;

namespace Shop.Infrastructure.Persistent.Ef.SiteEntities;

public class BannerConfiguration:IEntityTypeConfiguration<Banner>
{
    public void Configure(EntityTypeBuilder<Banner> builder)
    {
        builder.Property(x => x.ImageName)
            .HasMaxLength(120)
            .IsRequired();

        builder.Property(x => x.Link)
            .IsRequired()
            .HasMaxLength(500);
    }
}
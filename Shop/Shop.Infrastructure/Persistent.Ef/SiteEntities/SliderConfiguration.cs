using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.SiteEntities;

namespace Shop.Infrastructure.Persistent.Ef.SiteEntities;

public class SliderConfiguration:IEntityTypeConfiguration<Slider>
{
    public void Configure(EntityTypeBuilder<Slider> builder)
    {
        builder.Property(x => x.ImageName)
            .IsRequired()
            .HasMaxLength(120);

        builder.Property(x => x.Link)
            .IsRequired()
            .HasMaxLength(500);
    }
}
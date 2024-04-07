using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.RoleAgg;

namespace Shop.Infrastructure.Persistent.Ef.RoleAgg;

public class RoleConfiguration:IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Roles", "role");
        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(60);
        builder.OwnsMany(x => x.Permissions, option =>
        {
            option.ToTable("Permissions", "role");
        });
    }
}
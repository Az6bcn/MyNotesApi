using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Mappings;

public class SubscriptionMappings : IEntityTypeConfiguration<Subscription>
{
    public void Configure(EntityTypeBuilder<Subscription> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
               .UseIdentityColumn()
               .HasColumnType("int")
               .ValueGeneratedOnAdd()
               .IsRequired();

        builder.Property(p => p.Currency)
               .HasColumnType("varchar(50)")
               .IsUnicode(true)
               .ValueGeneratedNever()
               .IsRequired();

        builder.Property(p => p.Email)
               .HasColumnType("varchar(50)")
               .IsUnicode(true)
               .ValueGeneratedNever()
               .IsRequired();

        builder.Property(p => p.SubscriptionId)
               .HasColumnType("varchar(50)")
               .IsUnicode(false)
               .ValueGeneratedNever()
               .IsRequired();

        builder.Property(p => p.CustomerId)
               .HasColumnType("varchar(50)")
               .IsUnicode(false)
               .ValueGeneratedNever()
               .IsRequired();

        builder.Property(p => p.TrialExpired)
               .HasColumnType("datetimeoffset(7)")
               .ValueGeneratedNever()
               .IsRequired();
    }
}
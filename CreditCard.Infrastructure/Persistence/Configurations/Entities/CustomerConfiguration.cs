using CreditCard.Domain.Models.Entities;
using CreditCard.Domain.Models.ValueObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CreditCard.Infrastructure.Persistence.Configurations.Entities;

public class CustomerConfiguration: IEntityTypeConfiguration<CustomerEntity>
{
    public void Configure(EntityTypeBuilder<CustomerEntity> builder)
    {
        builder.ToTable("Customer");

        builder.ConfigureBaseEntity();

        builder.OwnsOne(x => x.Name, y =>
            {
                y.Property(y => y.First)
                    .HasColumnName("FirstName")
                    .IsRequired();

                y.Property(y => y.Last)
                    .HasColumnName("LastName")
                    .IsRequired();
            });

        builder.Property(x => x.MainEmail)
            .HasColumnName("MainEmail")
                .HasConversion(
                    x => x.Text,
                    text => new Email(text)
                ).IsRequired();
    }
}

using CreditCard.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CreditCard.Infrastructure.Persistence.Configurations.Entities;

public class CreditCardConfiguration : IEntityTypeConfiguration<CreditCardEntity>
{
    public void Configure(EntityTypeBuilder<CreditCardEntity> builder)
    {
        builder.ToTable("CreditCard");

        builder.ConfigureBaseEntity();
                
        builder.Property(x => x.CreditLimitReleased)
            .HasColumnName("CreditLimitReleased");

        // Configurando a relação sem expor CustomerId diretamente
        builder
            .HasOne(x => x.Customer)
            .WithMany()
            .HasForeignKey("CustomerId") // Use a chave estrangeira nomeada "CustomerId" que será gerada
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex("CustomerId");

        builder.Property(x => x.CreditLimitReleased)
            .HasColumnName("ProposalCreditId"); // is an external ms foreignKey

        builder.HasIndex("ProposalCreditId");

        builder.Property(x => x.Name)
            .HasColumnName("Name");
        
        builder.Property(x => x.Description)
            .HasColumnName("Description");

        builder.Property(x => x.Flag)
            .HasColumnName("Flag");
            
        builder.Property(x => x.IsActive)
            .HasColumnName("IsActive");
    }
}

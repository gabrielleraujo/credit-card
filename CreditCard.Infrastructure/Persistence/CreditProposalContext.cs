using CreditCard.Domain.Models.Entities;
using CreditCard.Infrastructure.Persistence.Configurations.Entities;
using Microsoft.EntityFrameworkCore;

namespace CreditCard.Infrastructure.Persistence;

public class CreditCardContext: DbContext
{
    public CreditCardContext(DbContextOptions options) : base(options) { }
    
    public DbSet<CustomerEntity> Customers { get; set; }
    public DbSet<CreditCardEntity> CreditCards { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CustomerConfiguration());
        modelBuilder.ApplyConfiguration(new CreditCardConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
using CreditCard.Domain.Models.Entities;

namespace CreditCard.Domain.Repositories;

public interface ICreditCardRepository
{
    #region commands
    Task AddAsync(CreditCardEntity customerEntity);
    void Update(CreditCardEntity customerEntity);
    void Delete(Guid CustomerEntityId);

    Task AddCustomerAsync(CustomerEntity customerEntity);

    #endregion
    
    #region queries
    Task<CreditCardEntity> FindByAsync(Func<CreditCardEntity, bool> predicate);
    Task<List<CreditCardEntity>> GetAllAsync();

    Task<CustomerEntity> FindCustomerByAsync(Func<CustomerEntity, bool> predicate);
    Task<List<CustomerEntity>> GetAllCustomersAsync();
    Task<List<CreditCardEntity>> GetAllCreditCardsByCustomerId(Guid customerId);
    #endregion

    Task CommitAsync();
    void Dispose();
}

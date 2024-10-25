using CreditCard.Domain.Models.Entities;
using CreditCard.Domain.Repositories;
using CreditCard.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CreditCard.Infrastructure.Repositories;
public class CreditCardRepository : ICreditCardRepository
{
    protected CreditCardContext _context;
    public CreditCardRepository(CreditCardContext context)
    {
        _context = context;
    }

    public async Task AddAsync(CreditCardEntity customer)
    {
        await _context.CreditCards.AddAsync(customer);
    }

    public void Update(CreditCardEntity customer)
    {
        _context.CreditCards.Update(customer);
    }

    public void Delete(Guid customerId)
    {
        var customer = _context.CreditCards.First(x => x.Id == customerId);
        _context.CreditCards.Attach(customer);
        _context.CreditCards.Remove(customer);
    }

    public async Task AddCustomerAsync(CustomerEntity customer)
    {
        await _context.Customers.AddAsync(customer);
    }

    #region consulting
    public async Task<CreditCardEntity> FindByAsync(Func<CreditCardEntity, bool> predicate)
    {
        var response = _context.CreditCards
            .FirstOrDefault(predicate);

        return await Task.FromResult(response!);
    }

    public async Task<List<CreditCardEntity>> GetAllAsync()
    {
        return await _context.CreditCards
            .ToListAsync();
    }


    public async Task<CustomerEntity> FindCustomerByAsync(Func<CustomerEntity, bool> predicate)
    {
        var response = _context.Customers
            .FirstOrDefault(predicate);

        return await Task.FromResult(response!);
    }

    public async Task<List<CustomerEntity>> GetAllCustomersAsync()
    {
        return await _context.Customers
            .ToListAsync();
    }

    public async Task<List<CreditCardEntity>> GetAllCreditCardsByCustomerId(Guid customerId) // Na lógica inicial só será possível 1 cartao por cliente, mas já permitindo suporte para mais de 1.
    {
        return await _context.CreditCards
            .Where(c => c.Customer.Id == customerId)
            .ToListAsync();
    }
    #endregion

    public void Dispose()
    {
        _context.Dispose();
    }

    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }
}

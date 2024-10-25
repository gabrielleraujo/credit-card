using CreditCard.Domain.Models.Abstracts;

namespace CreditCard.Domain.Models.Entities;

public class CreditCardEntity : BaseEntity
{
    private CreditCardEntity() {}
    public CreditCardEntity(
        Guid id,
        Guid proposalCreditId,
        decimal creditLimitReleased,
        CustomerEntity customer) : base(id)
    {
        ProposalCreditId = proposalCreditId;
        CreditLimitReleased = creditLimitReleased;
        Customer = customer;
        Validate();
        CustomerId = customer.Id;
    }

    public Guid CustomerId { get; private set; }
    public Guid ProposalCreditId { get; private set; }
    public CustomerEntity Customer { get; private set; }
    public decimal CreditLimitReleased { get; set; }
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public string Flag { get; private set; } = string.Empty;
    public bool IsActive { get; private set; }

    protected override void ApplyValidation()
    {
        if (Customer == null) AddError("The Customer cannot be null.");
        if (Customer!.Id == Guid.Empty) AddError("The Customer.Id cannot be null.");
        if (ProposalCreditId == null) AddError("The ProposalCreditId cannot be null.");
        if (CreditLimitReleased <= 0m) AddError("The CreditLimitReleased could not be less or equal to 0.");
    }

    /// <summary>
    /// Return true if has any category that fits.
    /// </summary>
    /// <returns></returns>
    public bool ChooseCreditCard()
    {
        if (CreditLimitReleased <= 5000m)
        {
            IsActive = true;
            Name = "Standard";
            Flag = "Visa";
            Description = "Cartão de crédito inicial.";
        }
        if (CreditLimitReleased > 5000m)
        {
            IsActive = true;
            Name = "Platinum";
            Flag = "Visa";
            Description = "Cartão de crédito intermediário.";
        }
        return IsActive;
    }
}

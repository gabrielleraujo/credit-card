using CreditCard.Domain.Models.Abstracts;
using CreditCard.Domain.Models.ValueObject;

namespace CreditCard.Domain.Models.Entities;

public class CustomerEntity : BaseEntity
{
    private CustomerEntity() {}
    public CustomerEntity(
        Guid id,
        Name name, 
        Email mainEmail
        ) : base(id)
    {
        Name = name;
        MainEmail = mainEmail;
        Validate();
    }

    public Name Name { get; private set; }
    public Email MainEmail { get; private set; }

    protected override void ApplyValidation()
    {
        if (Id == null)
        {
            AddError("The Id cannot be null.");
        }
    }
}

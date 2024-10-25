using CreditCard.Application.Commands.AddCreditCardToCustumerCommand;
using CreditCard.Application.Validations.Utils;
using FluentValidation;

namespace CreditCard.Application.Validations;

public class AddCreditCardToCustumerCommandValidation : AbstractValidator<AddCreditCardToCustumerCommand>
{
    public AddCreditCardToCustumerCommandValidation()
    {
        RuleFor(c => c.Event.CustomerId).ValidateNullOrEmpty("CustomerId");
        RuleFor(c => c.Event.Email).ValidateNullOrEmpty("Email");
    }
}

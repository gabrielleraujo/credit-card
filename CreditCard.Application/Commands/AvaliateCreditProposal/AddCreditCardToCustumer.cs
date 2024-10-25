using CreditCard.Application.Messaging.CustomerMainAddressRegistered.ConsumeEvent;
using CreditCard.Application.Validations;
using FluentValidation.Results;
using MediatR;

namespace CreditCard.Application.Commands.AddCreditCardToCustumerCommand
{
    public class AddCreditCardToCustumerCommand : Command, IRequest<ValidationResult>
    {
        public AddCreditCardToCustumerCommand(
            CreditCardHasCreditReleasedEvent @event)
        {
            Event = @event;
        }

        public CreditCardHasCreditReleasedEvent Event { get; private set; }

        public override bool IsValid() 
        {
            ValidationResult = new AddCreditCardToCustumerCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}

using System;
using MediatR;
using System.Text.Json;
using CreditCard.Domain.Repositories;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using CreditCard.Domain.Messaging;
using CreditCard.Domain.Models.ValueObject;
using CreditCard.Domain.Models.Entities;
using CreditCard.Application.Messaging.CustomerMainAddressRegistered.ConsumeEvent;

namespace CreditCard.Application.Commands.AddCreditCardToCustumerCommand
{
    public class AddCreditCardToCustumerCommandHandler : 
        CommandHandler<AddCreditCardToCustumerCommandHandler>,
        IRequestHandler<AddCreditCardToCustumerCommand, ValidationResult>
    {
        private readonly IMediator _mediator;
        private readonly ICreditCardRepository _repository;
        private readonly IMessageBusPublisher _messageBus;

        public AddCreditCardToCustumerCommandHandler(
            ILogger<AddCreditCardToCustumerCommandHandler> logger,
            IMediator mediator,
            ICreditCardRepository repository,
            IMessageBusPublisher messageBus) : base(logger)
        {
            _mediator = mediator;
            _repository = repository;
            _messageBus = messageBus;
        }
        
        public async Task<ValidationResult> Handle(AddCreditCardToCustumerCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{nameof(AddCreditCardToCustumerCommandHandler)} starting");

            if (!command.IsValid())
            {
                AddError("Command model is invalid\nEnd steps.");
                return command.ValidationResult;
            }

            var customer = await _repository.FindCustomerByAsync(x => x.Id == command.Event.CustomerId);

            if (customer == null)
            {
                _logger.LogInformation($"{nameof(AddCreditCardToCustumerCommandHandler)} Customer not registered yet.");

                // ---- Step Register Customer if it was not regitered.
                customer = new CustomerEntity(
                    id: command.Event.CustomerId,
                    name: new Name(command.Event.FirstName, command.Event.LastName),
                    mainEmail: new Email(command.Event.Email)
                );
                await _repository.AddCustomerAsync(customer);

                _logger.LogInformation($"{nameof(AddCreditCardToCustumerCommandHandler)} step register customer successfully added not commited");
            }
            
            // ---- Step Generate Credit Card to the customer
            var creditCard = new CreditCardEntity(
                id: Guid.NewGuid(),
                proposalCreditId: command.Event.ProposalCreditId,
                creditLimitReleased: command.Event.CreditLimitReleased,
                customer: customer
            );

            // TODO: Verificar no banco se o cliente já possui uma oferta liberada. Se tiver => nao continuar o processo. Se nao tiver => AvaliateCreditOffer.
            var limitNumberOfCards = 3;
            var creditCardDB = await _repository.GetAllCreditCardsByCustomerId(command.Event.CustomerId);
            if (creditCardDB != null && creditCardDB.Count >= limitNumberOfCards)
            {
                var message = $"The customer has already reached the limit of credit cards available per customer, the limit is {limitNumberOfCards} credit cards, the customer has: {creditCardDB.Count}";
                _logger.LogInformation($"{nameof(AddCreditCardToCustumerCommandHandler)} {message}\nEnd steps.");
                AddError(message);
                return ValidationResult;
            }

            var fitsAnyCategory = creditCard.ChooseCreditCard();

            if (fitsAnyCategory == false)
            {
                _logger.LogInformation($"{nameof(AddCreditCardToCustumerCommandHandler)} step choose credit card completed (Based on the credit analysis received from the customer, we did not find any credit card category that fits.)\nEnd steps.");
                return ValidationResult;
            }

            _logger.LogInformation($"{nameof(AddCreditCardToCustumerCommandHandler)} step choose credit card completed (Based on the credit analysis received from the customer, we choose the credit card {creditCard.Name.ToUpper()}.) with creditLimitReleased {creditCard.CreditLimitReleased}");

            _logger.LogInformation($"{nameof(AddCreditCardToCustumerCommandHandler)} Add new Credit Card to the customer {command.Event.CustomerId}");
            
            await _repository.AddAsync(creditCard);
            await _repository.CommitAsync();

            _logger.LogInformation($"{nameof(AddCreditCardToCustumerCommandHandler)} All steps was successfully completed\nEnd steps.");

            return ValidationResult;
        }
    }
}

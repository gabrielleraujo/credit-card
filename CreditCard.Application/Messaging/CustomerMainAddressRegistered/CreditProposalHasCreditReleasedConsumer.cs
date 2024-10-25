using System;
using Newtonsoft.Json;
using Polly;
using MediatR;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using CreditCard.Application.Messaging.CustomerMainAddressRegistered.ConsumeEvent;
using CreditCard.Application.Commands.AddCreditCardToCustumerCommand;
using FluentValidation.Results;

namespace CreditCard.Application.Messaging.CustomerMainAddressRegistered
{
    public class CreditProposalHasCreditReleasedConsumer : RabbitMqConsumerBackgroundServiceTemplate, IDisposable
    {
        private const string _routingKeySubscribe = "credit_proposal_has_credit_released_event_key";

        public CreditProposalHasCreditReleasedConsumer(
            IConnection connection, IModel channel, ILogger<CreditProposalHasCreditReleasedConsumer> logger, IServiceProvider serviceProvider)
            : base(connection, channel, logger, serviceProvider,
                  exchange: "customer-service-exchange", // Mesma exchange do outro microsserviço
                  queue: "add_credit_card_to_custumer_queue", // 
                  queueDeadLetter: "add_credit_card_to_custumer_dead_letter_queue")
        {
            // Vincular a fila ao exchange com a chave de roteamento "credit_proposal_has_credit_released_event_key"
            _channel.QueueBind(queue: _queue, 
                              exchange: _exchange, 
                              routingKey: _routingKeySubscribe);
        }

        protected override async Task<ValidationResult> ProcessMessage(string message)
        {   
            _logger.LogInformation($"{nameof(CreditProposalHasCreditReleasedConsumer)} - START ===============================================");
         
            // Tentar processar a mensagem
            _logger.LogInformation("Processando a geração de cartão de crédito: {message}", message);

            // Simular falha no processamento
            // throw new Exception("Falha simulada!");

            var @event = JsonConvert.DeserializeObject<CreditCardHasCreditReleasedEvent>(message);

            // // Lógica para verificar e processar a liberação de crédito
            using var scope = _serviceProvider.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            var command = new AddCreditCardToCustumerCommand(@event);
            var response = await mediator.Send(command);
            _logger.LogInformation("Finalizado o processamento de geração de cartão de crédito para o cliente: {CustomerId}, response isValid: {IsValid}", @event.CustomerId, response.IsValid);
            _logger.LogInformation($"{nameof(CreditProposalHasCreditReleasedConsumer)} - END ===============================================");
            return response;
        }

        protected virtual void Dispose(bool disposing)
        {
            RunDispose(disposing);
        }
    }
}

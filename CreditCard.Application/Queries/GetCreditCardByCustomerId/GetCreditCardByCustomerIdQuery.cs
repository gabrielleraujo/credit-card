using System.Text.Json.Serialization;
using CreditCard.Application.ViewModels;
using MediatR;

namespace CreditCard.Application.Queries.GetCreditCardByCustomerId
{
    public class GetCreditCardByCustomerIdQuery: IRequest<IEnumerable<CreditCardViewModel>>
    {
        public GetCreditCardByCustomerIdQuery(Guid customerId)
        {
            CustomerId = customerId;
        }

        [JsonPropertyName("customer_id")]
        public Guid CustomerId { get; private set; }
    }
}
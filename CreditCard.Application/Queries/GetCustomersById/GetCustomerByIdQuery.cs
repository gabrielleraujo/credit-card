using System.Text.Json.Serialization;
using CreditCard.Application.ViewModels;
using MediatR;

namespace CreditCard.Application.Queries.GetCustomersById
{
    public class GetCustomerByIdQuery: IRequest<CustomerViewModel?>
    {
        public GetCustomerByIdQuery(Guid customerId)
        {
            CustomerId = customerId;
        }

        [JsonPropertyName("customer_id")]
        public Guid CustomerId { get; private set; }
    }
}

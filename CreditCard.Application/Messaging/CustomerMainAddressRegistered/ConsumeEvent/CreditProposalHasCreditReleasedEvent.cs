using System.Text.Json.Serialization;

namespace CreditCard.Application.Messaging.CustomerMainAddressRegistered.ConsumeEvent
{
    public class CreditCardHasCreditReleasedEvent
    {
        [JsonPropertyName("customer_id")]
        public Guid CustomerId { get; set; }

        [JsonPropertyName("customer_id")]
        public Guid ProposalCreditId { get; set; }

        [JsonPropertyName("first_name")]
        public string FirstName { get; set; } = string.Empty;

        [JsonPropertyName("last_name")]
        public string LastName { get; set; } = string.Empty;

        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;

        [JsonPropertyName("credit-limit-released")]
        public decimal CreditLimitReleased { get; set; } // baseado no valor do limite o ms de cartao irá escolher o cartao de crédito que será liberado.

        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }
    }
}
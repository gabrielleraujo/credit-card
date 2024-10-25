namespace CreditCard.Domain.Messaging
{
    public interface IMessageBusPublisher
    {
        void PublishMessage(object data, string routingKey);
    }
}
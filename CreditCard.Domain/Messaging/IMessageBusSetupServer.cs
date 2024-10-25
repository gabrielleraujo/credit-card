namespace CreditCard.Infrastructure.Messaging
{
    public interface IMessageBusSetupServer
    {
        void Setup();
        void Dispose();
    }
}

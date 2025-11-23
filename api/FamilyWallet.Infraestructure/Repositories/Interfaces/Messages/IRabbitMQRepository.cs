namespace FamilyWallet.Infraestructure.Repositories.Interfaces
{
    public interface IRabbitMQRepository
    {
        Task PublishAsync<T>(T message, string queueName);
        Task StartConsumingAsync<T>(string queueName, Func<T, Task> handler);
    }
}
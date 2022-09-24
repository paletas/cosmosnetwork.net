namespace CosmosNetwork.Tests.Integration
{
    public interface IIntegrationTest
    {
        string Name { get; }

        Task Execute(IWallet wallet, CancellationToken cancellationToken = default);
    }
}
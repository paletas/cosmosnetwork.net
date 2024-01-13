namespace CosmosNetwork.Tests.Integration.Modules.API
{
    public interface IApiIntegrationTest
    {
        string Name { get; }

        Task Execute(IWallet wallet, CancellationToken cancellationToken = default);
    }
}
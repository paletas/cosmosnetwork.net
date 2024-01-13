namespace CosmosNetwork.Tests.Integration.Modules
{
    public interface IModule
    {
        string Name { get; }

        Task Execute(CancellationToken cancellationToken);
    }
}

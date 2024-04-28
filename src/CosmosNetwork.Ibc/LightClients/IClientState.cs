namespace CosmosNetwork.Ibc.LightClients
{
    public interface IClientState
    {
        string GetBlockchainId();

        ulong GetHeight();

        Serialization.LightClients.IClientState ToSerialization();
    }
}

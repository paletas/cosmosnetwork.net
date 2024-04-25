namespace CosmosNetwork.Ibc.LightClients
{
    public interface IClientState
    {
        ulong GetHeight();

        Serialization.LightClients.IClientState ToSerialization();
    }
}

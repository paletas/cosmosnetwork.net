namespace CosmosNetwork.Ibc.LightClients
{
    public interface IClientState
    {
        Serialization.LightClients.IClientState ToSerialization();
    }
}

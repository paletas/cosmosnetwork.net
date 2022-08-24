namespace CosmosNetwork.Ibc.LightClients
{
    public interface IHeader
    {
        Serialization.LightClients.IHeader ToSerialization();
    }
}

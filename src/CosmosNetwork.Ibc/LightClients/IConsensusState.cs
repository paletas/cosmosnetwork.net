namespace CosmosNetwork.Ibc.LightClients
{
    public interface IConsensusState
    {
        Serialization.LightClients.IConsensusState ToSerialization();
    }
}

namespace CosmosNetwork.Ibc.LightClients.Tendermint
{
    public record Misbehaviour(
        string ClientId,
        IHeader Header1,
        IHeader Header2) : IMisbehaviour
    {
        public Serialization.LightClients.IMisbehaviour ToSerialization()
        {
            return new Serialization.LightClients.Tendermint.Misbehaviour(
                this.ClientId,
                this.Header1.ToSerialization(),
                this.Header2.ToSerialization());
        }
    }
}

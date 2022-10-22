using ProtoBuf;

namespace CosmosNetwork.Ibc.Serialization.LightClients.Tendermint
{
    [ProtoContract]
    internal record Misbehaviour(
        [property: ProtoMember(1, Name = "client_id")] string ClientId,
        [property: ProtoMember(2, Name = "header_1")] IHeader Header1,
        [property: ProtoMember(3, Name = "header_2")] IHeader Header2) : IMisbehaviour
    {
        public const string AnyType = "/ibc.lightclients.tendermint.v1.Misbehaviour";

        public string TypeUrl => AnyType;

        public Ibc.LightClients.IMisbehaviour ToModel()
        {
            return new Ibc.LightClients.Tendermint.Misbehaviour(
                this.ClientId,
                this.Header1.ToModel(),
                this.Header2.ToModel());
        }
    }
}

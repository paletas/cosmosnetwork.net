using CosmosNetwork.Ibc.Serialization.Core.Client;
using ProtoBuf;

namespace CosmosNetwork.Ibc.Serialization.LightClients.Localhost
{
    [ProtoContract]
    internal record ClientState(
        [property: ProtoMember(1, Name = "chain_id")] string ChainId,
        [property: ProtoMember(2, Name = "height")] Height Height) : IClientState
    {
        public const string AnyType = "/ibc.lightclients.localhost.v1.ClientState";

        public string TypeUrl => AnyType;

        public Ibc.LightClients.IClientState ToModel()
        {
            return new Ibc.LightClients.Localhost.ClientState(
                this.ChainId,
                this.Height.ToModel());
        }
    }
}

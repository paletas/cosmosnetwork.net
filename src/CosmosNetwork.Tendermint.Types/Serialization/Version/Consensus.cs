using ProtoBuf;

namespace CosmosNetwork.Tendermint.Types.Serialization.Version
{
    [ProtoContract]
    public record Consensus(
        [property: ProtoMember(1, Name = "block")] ulong Block,
        [property: ProtoMember(1, Name = "app")] ulong App)
    {
        public Types.Version.Consensus ToModel()
        {
            return new Types.Version.Consensus(this.Block, this.App);
        }
    }
}

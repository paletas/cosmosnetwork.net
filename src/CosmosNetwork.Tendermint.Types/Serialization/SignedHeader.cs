using ProtoBuf;

namespace CosmosNetwork.Tendermint.Types.Serialization
{
    [ProtoContract]
    public record SignedHeader(
        [property: ProtoMember(1, Name = "header")] Header Header,
        [property: ProtoMember(2, Name = "commit")] Commit Commit)
    {
        public Types.SignedHeader ToModel()
        {
            return new Types.SignedHeader(
                this.Header.ToModel(),
                this.Commit.ToModel());
        }
    }
}

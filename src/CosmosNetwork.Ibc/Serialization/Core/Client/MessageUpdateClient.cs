using CosmosNetwork.Serialization;
using CosmosNetwork.Serialization.Proto;
using ProtoBuf;

namespace CosmosNetwork.Ibc.Serialization.Core.Client
{
    [ProtoContract]
    internal record MessageUpdateClient(
        [property: ProtoMember(1, Name = "client_id")] string ClientId,
        [property: ProtoMember(3, Name = "signer")] string Signer) : SerializerMessage
    {
        [ProtoIgnore]
        public byte[] Header { get; set; } = null!;

        [ProtoMember(2, Name = "header")]
        public Any HeaderPacked
        {
            get => Any.Pack(null, this.Header);
            set => this.Header = value.ToArray();
        }

        protected override Message ToModel()
        {
            return new Ibc.Core.Client.MessageUpdateClient(
                this.ClientId,
                this.Header,
                this.Signer);
        }
    }
}

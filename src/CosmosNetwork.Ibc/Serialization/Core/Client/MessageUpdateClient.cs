using CosmosNetwork.Ibc.Serialization.LightClients;
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
        public IHeader Header { get; set; } = null!;

        [ProtoMember(2, Name = "header")]
        public Any HeaderPacked
        {
            get => Any.Pack(this.Header);
            set => this.Header = value.UnpackHeader();
        }

        protected override Message ToModel()
        {
            return new Ibc.Core.Client.MessageUpdateClient(
                this.ClientId,
                this.Header.ToModel(),
                this.Signer);
        }
    }
}

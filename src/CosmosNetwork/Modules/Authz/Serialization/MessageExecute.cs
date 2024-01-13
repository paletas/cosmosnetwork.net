using CosmosNetwork.Modules.Authz.Serialization.Json;
using CosmosNetwork.Serialization;
using CosmosNetwork.Serialization.Proto;
using ProtoBuf;
using System.Text.Json.Serialization;

namespace CosmosNetwork.Modules.Authz.Serialization
{
    [ProtoContract]
    internal record MessageExecute(
        [property: ProtoMember(1, Name = "grantee"), JsonPropertyName("grantee")] string GranteeAddress) : SerializerMessage(Authz.MessageExecute.COSMOS_DESCRIPTOR)
    {
        [ProtoIgnore, JsonPropertyName("msgs"), JsonConverter(typeof(AuthorizationsConverter))]
        public SerializerMessage[] Messages { get; set; } = null!;

        [ProtoMember(2, Name = "msgs")]
        public Any[] MessagesPack
        {
            get => this.Messages.Select(auth => Any.Pack(auth)).ToArray();
            set => this.Messages = value
                    .Select(msg => UnpackMessage(msg))
                    .ToArray();
        }

        private static SerializerMessage UnpackMessage(Any message)
        {
            Type? msgType = CosmosMessageRegistry.Instance.GetMessageType(message.TypeUrl);
            return msgType is null
                ? throw new InvalidOperationException($"unknown message type {message.TypeUrl}")
                : (SerializerMessage)(message.Unpack(msgType) ?? throw new InvalidOperationException());
        }

        public override Message ToModel()
        {
            return new Authz.MessageExecute(
                this.GranteeAddress,
                this.Messages.Select(msg => msg.ToModel()).ToArray());
        }
    }
}

using CosmosNetwork.Modules.Authz.Serialization.Authorizations;
using CosmosNetwork.Modules.Authz.Serialization.Json;
using CosmosNetwork.Serialization;
using ProtoBuf;
using System.Text.Json.Serialization;

namespace CosmosNetwork.Modules.Authz.Serialization
{
    [ProtoContract]
    internal record MessageExecute(
        [property: ProtoMember(1, Name = "grantee"), JsonPropertyName("grantee")] string GranteeAddress) : SerializerMessage
    {
        [ProtoIgnore, JsonPropertyName("msgs"), JsonConverter(typeof(AuthorizationsConverter))]
        public Authorizations.IAuthorization[] Authorizations { get; set; } = null!;

        [ProtoMember(2, Name = "msgs")]
        public Any[] MessagesPack
        {
            get => Authorizations.Select(auth => Any.Pack(auth)).ToArray();
            set => Authorizations = value
                    .Select(auth => UnpackAuthorization(auth))
                    .ToArray();
        }

        private static IAuthorization UnpackAuthorization(Any authorization)
        {
            var authorizationType = AuthorizationRegistry.Instance.GetProposalByTypeName(authorization.TypeUrl);

            return (IAuthorization) (authorization.Unpack(authorizationType) ?? throw new InvalidOperationException());
        }

        protected internal override Message ToModel()
        {
            return new Authz.MessageExecute(
                GranteeAddress,
                Authorizations.Select(msg => msg.ToModel()).ToArray());
        }
    }
}

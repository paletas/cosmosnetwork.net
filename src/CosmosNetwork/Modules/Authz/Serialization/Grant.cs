using CosmosNetwork.Modules.Authz.Serialization.Json;
using CosmosNetwork.Serialization.Proto;
using ProtoBuf;
using System.Text.Json.Serialization;

namespace CosmosNetwork.Modules.Authz.Serialization
{
    [ProtoContract]
    public class Grant
    {
        [ProtoIgnore]
        [JsonConverter(typeof(AuthorizationConverter))]
        public Serialization.Authorizations.IAuthorization Authorization { get; set; } = null!;

        [ProtoMember(1, Name = "authorization")]
        public Any AuthorizationPack
        {
            get => Any.Pack(this.Authorization);
            set => this.Authorization = value.Unpack<Authorizations.IAuthorization>() ?? throw new NotImplementedException();
        }

        [ProtoMember(2, Name = "expiration")]
        public DateTime Expiration { get; set; }

        public Authz.Grant ToModel()
        {
            return new CosmosNetwork.Modules.Authz.Grant
            {
                Authorization = this.Authorization.ToModel(),
                Expiration = Expiration
            };
        }
    }
}

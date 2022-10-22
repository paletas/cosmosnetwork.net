using CosmosNetwork.Modules.FeeGrant.Serialization.Allowances;
using CosmosNetwork.Modules.FeeGrant.Serialization.Allowances.Json;
using CosmosNetwork.Serialization;
using CosmosNetwork.Serialization.Proto;
using ProtoBuf;
using System.Text.Json.Serialization;

namespace CosmosNetwork.Modules.FeeGrant.Serialization
{
    [ProtoContract]
    internal record MessageGrantAllowance(
        [property: ProtoMember(1, Name = "granter"), JsonPropertyName("granter")] string GranterAddress,
        [property: ProtoMember(2, Name = "grantee"), JsonPropertyName("grantee")] string GranteeAddress) : SerializerMessage(FeeGrant.MessageGrantAllowance.COSMOS_DESCRIPTOR)
    {
        private Any? _allowancePack;
        public const string TERRA_DESCRIPTOR = "feegrant/MsgGrantAllowance";

        [ProtoIgnore, JsonConverter(typeof(AllowanceConverter))]
        public IAllowance? Allowance { get; set; }

        [ProtoMember(3, Name = "allowance")]
        public Any AllowancePack
        {
            get => _allowancePack ??= Any.Pack(Allowance ?? throw new InvalidOperationException());
            set => _allowancePack = value;
        }

        [ProtoAfterDeserialization]
        public void OnDeserialization()
        {
            switch (AllowancePack.TypeUrl)
            {
                case BasicAllowance.AllowanceType:
                    Allowance = AllowancePack.Unpack<BasicAllowance>();
                    break;
                case PeriodicAllowance.AllowanceType:
                    Allowance = AllowancePack.Unpack<PeriodicAllowance>();
                    break;
                case AllowedMessageAllowance.AllowanceType:
                    Allowance = AllowancePack.Unpack<AllowedMessageAllowance>();
                    break;
            }
        }

        protected internal override Message ToModel()
        {
            return new CosmosNetwork.Modules.FeeGrant.MessageGrantAllowance(GranterAddress, GranteeAddress, Allowance.ToModel());
        }
    }
}

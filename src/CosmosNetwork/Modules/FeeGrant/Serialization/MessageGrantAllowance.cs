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
            get => this._allowancePack ??= Any.Pack(this.Allowance ?? throw new InvalidOperationException());
            set => this._allowancePack = value;
        }

        [ProtoAfterDeserialization]
        public void OnDeserialization()
        {
            switch (this.AllowancePack.TypeUrl)
            {
                case BasicAllowance.AllowanceType:
                    this.Allowance = this.AllowancePack.Unpack<BasicAllowance>();
                    break;
                case PeriodicAllowance.AllowanceType:
                    this.Allowance = this.AllowancePack.Unpack<PeriodicAllowance>();
                    break;
                case AllowedMessageAllowance.AllowanceType:
                    this.Allowance = this.AllowancePack.Unpack<AllowedMessageAllowance>();
                    break;
            }
        }

        protected internal override Message ToModel()
        {
            return new CosmosNetwork.Modules.FeeGrant.MessageGrantAllowance(this.GranterAddress, this.GranteeAddress, this.Allowance.ToModel());
        }
    }
}

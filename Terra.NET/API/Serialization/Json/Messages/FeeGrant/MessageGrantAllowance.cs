using Cosmos.SDK.Protos.FeeGrant;
using Google.Protobuf;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Terra.NET.API.Serialization.Json.Messages.FeeGrant
{
    [MessageDescriptor(TerraType = TERRA_DESCRIPTOR, CosmosType = COSMOS_DESCRIPTOR)]
    internal record MessageGrantAllowance([property: JsonPropertyName("granter")] string GranterAddress, [property: JsonPropertyName("grantee")] string GranteeAddress, IAllowance Allowance)
        : Message(TERRA_DESCRIPTOR, COSMOS_DESCRIPTOR)
    {
        public const string TERRA_DESCRIPTOR = "feegrant/MsgGrantAllowance";
        public const string COSMOS_DESCRIPTOR = "/cosmos.feegrant.v1beta1.MsgGrantAllowance";

        internal override IMessage ToProto(JsonSerializerOptions? serializerOptions = null)
        {
            return new MsgGrantAllowance
            {
                Granter = this.GranterAddress,
                Grantee = this.GranteeAddress,
                Allowance = this.Allowance.PackAny()
            };
        }

        internal override NET.Message ToModel()
        {
            return new NET.Messages.FeeGrant.MessageGrantAllowance(this.GranterAddress, this.GranteeAddress, this.Allowance.ToModel());
        }
    }

    internal interface IAllowance
    {
        Google.Protobuf.WellKnownTypes.Any PackAny();

        NET.Messages.FeeGrant.Allowance ToModel();
    }

    internal record BasicAllowance(DenomAmount[] SpendLimit, DateTime? Expiration) : IAllowance
    {
        internal Cosmos.SDK.Protos.FeeGrant.BasicAllowance ToProto()
        {
            Cosmos.SDK.Protos.FeeGrant.BasicAllowance allowance = new();
            if (this.Expiration.HasValue) allowance.Expiration = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(this.Expiration.Value);
            allowance.SpendLimit.AddRange(this.SpendLimit.Select(sl => sl.ToProto()));
            return allowance;
        }

        public Google.Protobuf.WellKnownTypes.Any PackAny()
        {
            var allowance = ToProto();
            return new Google.Protobuf.WellKnownTypes.Any
            {
                TypeUrl = Cosmos.SDK.Protos.FeeGrant.BasicAllowance.Descriptor.Name,
                Value = allowance.ToByteString()
            };
        }

        public NET.Messages.FeeGrant.Allowance ToModel()
        {
            return new NET.Messages.FeeGrant.BasicAllowance(
                this.SpendLimit.Select(coin => coin.ToModel()).ToArray(),
                this.Expiration);
        }
    }

    internal record PeriodicAllowance(BasicAllowance Basic, TimeSpan Period, DenomAmount[] PeriodSpendLimit, DenomAmount[] PeriodCanSpend, DateTime PeriodReset) : IAllowance
    {
        public Google.Protobuf.WellKnownTypes.Any PackAny()
        {
            Cosmos.SDK.Protos.FeeGrant.PeriodicAllowance allowance = new()
            {
                Basic = this.Basic.ToProto(),
                Period = Google.Protobuf.WellKnownTypes.Duration.FromTimeSpan(this.Period),
                PeriodReset = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(this.PeriodReset)
            };
            allowance.PeriodSpendLimit.AddRange(this.PeriodSpendLimit.Select(ps => ps.ToProto()));
            allowance.PeriodCanSpend.AddRange(this.PeriodCanSpend.Select(pc => pc.ToProto()));

            return new Google.Protobuf.WellKnownTypes.Any
            {
                TypeUrl = Cosmos.SDK.Protos.FeeGrant.PeriodicAllowance.Descriptor.Name,
                Value = allowance.ToByteString()
            };
        }

        public NET.Messages.FeeGrant.Allowance ToModel()
        {
            return new NET.Messages.FeeGrant.PeriodicAllowance(
                (NET.Messages.FeeGrant.BasicAllowance)this.Basic.ToModel(),
                this.Period,
                this.PeriodSpendLimit.Select(coin => coin.ToModel()).ToArray(),
                this.PeriodCanSpend.Select(coin => coin.ToModel()).ToArray(),
                this.PeriodReset);
        }
    }

    internal record AllowedMsgAllowance(IAllowance Allowance, string[] AllowedMessages) : IAllowance
    {
        public Google.Protobuf.WellKnownTypes.Any PackAny()
        {
            Cosmos.SDK.Protos.FeeGrant.AllowedMsgAllowance allowance = new()
            {
                Allowance = this.Allowance.PackAny()
            };
            allowance.AllowedMessages.AddRange(this.AllowedMessages);

            return new Google.Protobuf.WellKnownTypes.Any
            {
                TypeUrl = Cosmos.SDK.Protos.FeeGrant.AllowedMsgAllowance.Descriptor.Name,
                Value = allowance.ToByteString()
            };
        }

        public NET.Messages.FeeGrant.Allowance ToModel()
        {
            return new NET.Messages.FeeGrant.AllowedMessageAllowance(
                this.Allowance.ToModel(),
                this.AllowedMessages);
        }
    }
}

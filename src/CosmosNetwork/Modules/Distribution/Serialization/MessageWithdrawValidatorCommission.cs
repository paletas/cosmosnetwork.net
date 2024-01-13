using CosmosNetwork.Serialization;
using ProtoBuf;

namespace CosmosNetwork.Modules.Distribution.Serialization
{
    [ProtoContract]
    public record MessageWithdrawValidatorCommission(
        [property: ProtoMember(1, Name = "validator_address")] string ValidatorAddress) : SerializerMessage(Distribution.MessageWithdrawValidatorCommission.COSMOS_DESCRIPTOR)
    {
        public const string TERRA_DESCRIPTOR = "distribution/MsgWithdrawValidatorCommission";

        public override Message ToModel()
        {
            return new Distribution.MessageWithdrawValidatorCommission(this.ValidatorAddress);
        }
    }
}

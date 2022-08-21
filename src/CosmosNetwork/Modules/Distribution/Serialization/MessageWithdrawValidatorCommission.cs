using CosmosNetwork.Serialization;
using ProtoBuf;

namespace CosmosNetwork.Modules.Distribution.Serialization
{
    [ProtoContract]
    internal record MessageWithdrawValidatorCommission(
        [property: ProtoMember(1, Name = "validator_address")] string ValidatorAddress) : SerializerMessage
    {
        public const string TERRA_DESCRIPTOR = "distribution/MsgWithdrawValidatorCommission";

        protected internal override Message ToModel()
        {
            return new Distribution.MessageWithdrawValidatorCommission(ValidatorAddress);
        }
    }
}

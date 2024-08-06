using CosmosNetwork.Modules.FeeGrant.Allowances;
using CosmosNetwork.Serialization;

namespace CosmosNetwork.Modules.FeeGrant
{
    public record MessageGrantAllowance(
        string MessageType,
        CosmosAddress Granter,
        CosmosAddress Grantee,
        IAllowance Allowance) : Message(MessageType)
    {
        public const string COSMOS_DESCRIPTOR = "/cosmos.feegrant.v1beta1.MsgGrantAllowance";

        public override SerializerMessage ToSerialization()
        {
            return new Serialization.MessageGrantAllowance(
                this.Granter.Address,
                this.Grantee.Address)
            {
                Allowance = this.Allowance.ToSerialization()
            };
        }
    }
}
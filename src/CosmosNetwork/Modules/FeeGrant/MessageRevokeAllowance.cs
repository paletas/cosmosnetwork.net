﻿namespace CosmosNetwork.Modules.FeeGrant
{
    [CosmosMessage(COSMOS_DESCRIPTOR)]
    public record MessageRevokeAllowance(CosmosAddress Granter, CosmosAddress Grantee) : Message
    {
        public const string COSMOS_DESCRIPTOR = "/cosmos.feegrant.v1beta1.MsgRevokeAllowance";

        public override CosmosNetwork.Serialization.SerializerMessage ToSerialization()
        {
            return new Serialization.MessageRevokeAllowance(
                this.Granter.Address,
                this.Grantee.Address);
        }
    }
}
﻿namespace CosmosNetwork.Modules.Distribution
{
    [CosmosMessage(COSMOS_DESCRIPTOR)]
    public record MessageSetWithdrawAddress(CosmosAddress Delegator, CosmosAddress Withdraw) : Message
    {
        public const string COSMOS_DESCRIPTOR = "/cosmos.distribution.v1beta1.MsgSetWithdrawAddress";

        public override CosmosNetwork.Serialization.SerializerMessage ToSerialization()
        {
            return new Serialization.MessageSetWithdrawAddress(this.Delegator.Address, this.Withdraw.Address);
        }
    }
}
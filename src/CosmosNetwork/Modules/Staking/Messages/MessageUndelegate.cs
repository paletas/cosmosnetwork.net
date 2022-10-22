﻿namespace CosmosNetwork.Modules.Staking.Messages
{
    [CosmosMessage(COSMOS_DESCRIPTOR)]
    public record MessageUndelegate(CosmosAddress Delegator, CosmosAddress Validator, Coin Amount) : Message
    {
        public const string COSMOS_DESCRIPTOR = "/cosmos.staking.v1beta1.MsgUndelegate";

        protected internal override CosmosNetwork.Serialization.SerializerMessage ToSerialization()
        {
            return new Serialization.MessageUndelegate(
                Delegator.Address,
                Validator.Address,
                Amount.ToSerialization());
        }
    }
}
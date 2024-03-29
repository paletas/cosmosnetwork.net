﻿using CosmosNetwork.Serialization;

namespace CosmosNetwork.Ibc.Applications.Fees
{
    [CosmosMessage(COSMOS_DESCRIPTOR)]
    public record MessageRegisterCounterpartyAddress(
        CosmosAddress Address,
        CosmosAddress CounterpartyAddress,
        string ChannelId) : Message
    {
        public const string COSMOS_DESCRIPTOR = "/ibc.applications.fee.v1.MsgRegisterCounterpartyAddress";

        public override SerializerMessage ToSerialization()
        {
            return new Serialization.Applications.Fees.MessageRegisterCounterpartyAddress(
                this.Address.Address,
                this.CounterpartyAddress.Address,
                this.ChannelId);
        }
    }
}

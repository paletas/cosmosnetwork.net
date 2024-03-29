﻿using CosmosNetwork.Serialization;
using ProtoBuf;

namespace CosmosNetwork.Ibc.Serialization.Applications.Fees
{
    [ProtoContract]
    internal record Fee(
        [property: ProtoMember(1, Name = "recv_fee")] DenomAmount[] ReceiverFee,
        [property: ProtoMember(2, Name = "ack_fee")] DenomAmount[] AcknowledgeFee,
        [property: ProtoMember(3, Name = "timeout_fee")] DenomAmount[] TimeoutFee)
    {
        public Ibc.Applications.Fees.Fee ToModel()
        {
            return new Ibc.Applications.Fees.Fee(
                this.ReceiverFee.Select(fee => fee.ToModel()).ToArray(),
                this.AcknowledgeFee.Select(fee => fee.ToModel()).ToArray(),
                this.TimeoutFee.Select(fee => fee.ToModel()).ToArray());
        }
    }
}

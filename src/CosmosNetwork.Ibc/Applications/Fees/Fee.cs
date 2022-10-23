namespace CosmosNetwork.Ibc.Applications.Fees
{
    public record Fee(
        Coin[] ReceiverFee,
        Coin[] AcknowledgeFee,
        Coin[] TimeoutFee)
    {
        internal Serialization.Applications.Fees.Fee ToSerialization()
        {
            return new Serialization.Applications.Fees.Fee(
                this.ReceiverFee.Select(fee => fee.ToSerialization()).ToArray(),
                this.AcknowledgeFee.Select(fee => fee.ToSerialization()).ToArray(),
                this.TimeoutFee.Select(fee => fee.ToSerialization()).ToArray());
        }
    }
}

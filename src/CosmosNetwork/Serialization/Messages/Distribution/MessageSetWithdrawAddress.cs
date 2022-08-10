namespace CosmosNetwork.Serialization.Messages.Distribution
{
    internal record MessageSetWithdrawAddress(string DelegatorAddress, string WithdrawAddress) : SerializerMessage
    {
        protected internal override Message ToModel()
        {
            return new CosmosNetwork.Messages.Distribution.MessageSetWithdrawAddress(DelegatorAddress, WithdrawAddress);
        }
    }
}

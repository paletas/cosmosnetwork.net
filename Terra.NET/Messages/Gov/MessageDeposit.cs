namespace Terra.NET.Messages.Gov
{
    public record MessageDeposit(ulong ProposalId, TerraAddress Depositor, Coin[] Coins)
        : Message(MessageTypeEnum.GovDeposit)
    {
        internal override API.Serialization.Json.Message ToJson()
        {
            return new API.Serialization.Json.Messages.Gov.MessageDeposit(
                this.ProposalId,
                this.Depositor.Address,
                this.Coins.Select(coin => new API.Serialization.Json.DenomAmount(coin.Denom, coin.Amount)).ToArray());
        }
    }
}
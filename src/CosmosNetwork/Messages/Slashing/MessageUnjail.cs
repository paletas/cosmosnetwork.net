namespace CosmosNetwork.Messages.Slashing
{
    [CosmosMessage(COSMOS_DESCRIPTOR)]
    public record MessageUnjail(CosmosAddress Validator) : Message
    {
        public const string COSMOS_DESCRIPTOR = "/cosmos.slashing.v1beta1.MsgUnjail";

        internal override Serialization.SerializerMessage ToJson()
        {
            return new Serialization.Messages.Slashing.MessageUnjail(Validator.Address);
        }
    }
}
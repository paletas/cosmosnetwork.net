namespace CosmosNetwork.Modules.Slashing
{
    [CosmosMessage(COSMOS_DESCRIPTOR)]
    public record MessageUnjail(CosmosAddress Validator) : Message
    {
        public const string COSMOS_DESCRIPTOR = "/cosmos.slashing.v1beta1.MsgUnjail";

        protected internal override CosmosNetwork.Serialization.SerializerMessage ToSerialization()
        {
            return new Serialization.MessageUnjail(Validator.Address);
        }
    }
}
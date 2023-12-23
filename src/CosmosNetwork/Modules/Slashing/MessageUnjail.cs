namespace CosmosNetwork.Modules.Slashing
{
    [CosmosMessage(COSMOS_DESCRIPTOR)]
    public record MessageUnjail(CosmosAddress Validator) : Message
    {
        public const string COSMOS_DESCRIPTOR = "/cosmos.slashing.v1beta1.MsgUnjail";

        public override CosmosNetwork.Serialization.SerializerMessage ToSerialization()
        {
            return new Serialization.MessageUnjail(this.Validator.Address);
        }
    }
}
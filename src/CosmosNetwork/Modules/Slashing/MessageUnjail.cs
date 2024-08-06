namespace CosmosNetwork.Modules.Slashing
{
    public record MessageUnjail(string MessageType, CosmosAddress Validator) : Message(MessageType)
    {
        public const string COSMOS_DESCRIPTOR = "/cosmos.slashing.v1beta1.MsgUnjail";

        public override CosmosNetwork.Serialization.SerializerMessage ToSerialization()
        {
            return new Serialization.MessageUnjail(this.Validator.Address);
        }
    }
}
using CosmosNetwork.Serialization;
using ProtoBuf;

namespace CosmosNetwork.Modules.Bank.Serialization
{
    [ProtoContract]
    internal record MessageMultiSend(
        [property: ProtoMember(1, Name = "inputs")] MessageMultiSendInputOutput[] Inputs,
        [property: ProtoMember(2, Name = "outputs")] MessageMultiSendInputOutput[] Outputs) : SerializerMessage(Bank.MessageMultiSend.COSMOS_DESCRIPTOR)
    {
        protected internal override Message ToModel()
        {
            return new CosmosNetwork.Modules.Bank.MessageMultiSend(
                this.Inputs.Select(i => i.ToModel()).ToArray(),
                this.Outputs.Select(o => o.ToModel()).ToArray());
        }
    }

    [ProtoContract]
    internal record MessageMultiSendInputOutput(
        [property: ProtoMember(1, Name = "address")] string Address,
        [property: ProtoMember(2, Name = "coins")] DenomAmount[] Coins)
    {
        internal CosmosNetwork.Modules.Bank.MessageMultiSendInputOutput ToModel()
        {
            return new CosmosNetwork.Modules.Bank.MessageMultiSendInputOutput(
                this.Address,
                this.Coins.Select(c => c.ToModel()).ToArray());
        }
    }
}

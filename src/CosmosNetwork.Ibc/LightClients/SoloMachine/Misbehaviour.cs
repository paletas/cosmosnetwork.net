namespace CosmosNetwork.Ibc.LightClients.SoloMachine
{
    public record Misbehaviour(
        string ClientId,
        ulong Sequence,
        SignatureAndData SignatureOne,
        SignatureAndData SignatureTwo) : IMisbehaviour
    {
        public Serialization.LightClients.IMisbehaviour ToSerialization()
        {
            return new Serialization.LightClients.SoloMachine.Misbehaviour(
                this.ClientId,
                this.Sequence,
                this.SignatureOne.ToSerialization(),
                this.SignatureTwo.ToSerialization());
        }
    }

    public record SignatureAndData(
        byte[] Signature,
        DataTypeEnum DataType,
        byte[] Data,
        ulong Timestamp)
    {
        internal Serialization.LightClients.SoloMachine.SignatureAndData ToSerialization()
        {
            return new Serialization.LightClients.SoloMachine.SignatureAndData(
                this.Signature,
                (Serialization.LightClients.SoloMachine.DataTypeEnum)this.DataType,
                this.Data,
                this.Timestamp);
        }
    }

    public enum DataTypeEnum
    {
        Unspecified = Serialization.LightClients.SoloMachine.DataTypeEnum.Unspecified,

        ClientState = Serialization.LightClients.SoloMachine.DataTypeEnum.ClientState,

        ConsensustState = Serialization.LightClients.SoloMachine.DataTypeEnum.ConsensustState,

        ConnectionState = Serialization.LightClients.SoloMachine.DataTypeEnum.ConnectionState,

        ChannelState = Serialization.LightClients.SoloMachine.DataTypeEnum.ChannelState,

        PacketCommitment = Serialization.LightClients.SoloMachine.DataTypeEnum.PacketCommitment,

        PacketAcknowledgement = Serialization.LightClients.SoloMachine.DataTypeEnum.PacketAcknowledgement,

        PacketReceiptAbsence = Serialization.LightClients.SoloMachine.DataTypeEnum.PacketReceiptAbsence,

        NextSequenceRecv = Serialization.LightClients.SoloMachine.DataTypeEnum.NextSequenceRecv,

        Header = Serialization.LightClients.SoloMachine.DataTypeEnum.Header
    }
}

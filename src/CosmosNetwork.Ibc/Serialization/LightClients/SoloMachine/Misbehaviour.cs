﻿using ProtoBuf;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace CosmosNetwork.Ibc.Serialization.LightClients.SoloMachine
{
    [ProtoContract]
    internal record Misbehaviour(
        [property: ProtoMember(1, Name = "client_id")] string ClientId,
        [property: ProtoMember(2, Name = "sequence")] ulong Sequence,
        [property: ProtoMember(3, Name = "signature_one")] SignatureAndData SignatureOne,
        [property: ProtoMember(4, Name = "signature_two")] SignatureAndData SignatureTwo) : IMisbehaviour
    {
        public const string AnyType = "/ibc.lightclients.solomachine.v2.Misbehaviour";

        public string TypeUrl => AnyType;

        public Ibc.LightClients.IMisbehaviour ToModel()
        {
            return new Ibc.LightClients.SoloMachine.Misbehaviour(
                this.ClientId,
                this.Sequence,
                this.SignatureOne.ToModel(),
                this.SignatureTwo.ToModel());
        }
    }

    [ProtoContract]
    internal record SignatureAndData(
        [property: ProtoMember(1, Name = "signature")] byte[] Signature,
        [property: ProtoMember(2, Name = "data_type")] DataTypeEnum DataType,
        [property: ProtoMember(3, Name = "data")] byte[] Data,
        [property: ProtoMember(4, Name = "timestamp")] ulong Timestamp)
    {
        public Ibc.LightClients.SoloMachine.SignatureAndData ToModel()
        {
            return new Ibc.LightClients.SoloMachine.SignatureAndData(
                this.Signature,
                (Ibc.LightClients.SoloMachine.DataTypeEnum)this.DataType,
                this.Data,
                this.Timestamp);
        }
    }

    [ProtoContract]
    [JsonConverter(typeof(JsonStringEnumMemberConverter))]
    internal enum DataTypeEnum
    {
        [ProtoEnum(Name = "DATA_TYPE_UNINITIALIZED_UNSPECIFIED")]
        [EnumMember(Value = "DATA_TYPE_UNINITIALIZED_UNSPECIFIED")]
        Unspecified = 0,

        [ProtoEnum(Name = "DATA_TYPE_CLIENT_STATE")]
        [EnumMember(Value = "DATA_TYPE_CLIENT_STATE")]
        ClientState = 1,

        [ProtoEnum(Name = "DATA_TYPE_CONSENSUS_STATE")]
        [EnumMember(Value = "DATA_TYPE_CONSENSUS_STATE")]
        ConsensustState = 2,

        [ProtoEnum(Name = "DATA_TYPE_CONNECTION_STATE")]
        [EnumMember(Value = "DATA_TYPE_CONNECTION_STATE")]
        ConnectionState = 3,

        [ProtoEnum(Name = "DATA_TYPE_CHANNEL_STATE")]
        [EnumMember(Value = "DATA_TYPE_CHANNEL_STATE")]
        ChannelState = 4,

        [ProtoEnum(Name = "DATA_TYPE_PACKET_COMMITMENT")]
        [EnumMember(Value = "DATA_TYPE_PACKET_COMMITMENT")]
        PacketCommitment = 5,

        [ProtoEnum(Name = "DATA_TYPE_PACKET_ACKNOWLEDGEMENT")]
        [EnumMember(Value = "DATA_TYPE_PACKET_ACKNOWLEDGEMENT")]
        PacketAcknowledgement = 6,

        [ProtoEnum(Name = "DATA_TYPE_PACKET_RECEIPT_ABSENCE")]
        [EnumMember(Value = "DATA_TYPE_PACKET_RECEIPT_ABSENCE")]
        PacketReceiptAbsence = 7,

        [ProtoEnum(Name = "DATA_TYPE_NEXT_SEQUENCE_RECV")]
        [EnumMember(Value = "DATA_TYPE_NEXT_SEQUENCE_RECV")]
        NextSequenceRecv = 8,

        [ProtoEnum(Name = "DATA_TYPE_HEADER")]
        [EnumMember(Value = "DATA_TYPE_HEADER")]
        Header = 9
    }
}

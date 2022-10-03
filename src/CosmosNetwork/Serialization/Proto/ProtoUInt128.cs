using ProtoBuf;
using UltimateOrb;

namespace CosmosNetwork.Serialization.Proto
{
    [ProtoContract]
    internal class ProtoUInt128
    {
        [ProtoMember(1, Name = "lo")]
        public long Lo { get; set; }

        [ProtoMember(2, Name = "hi")]
        public long Hi { get; set; }

        public static implicit operator UInt128(ProtoUInt128 suggorage)
        {
            return UInt128.FromBits(suggorage.Lo, suggorage.Hi);
        }

        public static implicit operator ProtoUInt128(UInt128 source)
        {
            return new ProtoUInt128
            {
                Lo = source.LoInt64Bits,
                Hi = source.HiInt64Bits
            };
        }
    }
}

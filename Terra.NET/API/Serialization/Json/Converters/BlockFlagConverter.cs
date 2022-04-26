using System.Text.Json;
using System.Text.Json.Serialization;

namespace Terra.NET.API.Serialization.Json.Converters
{
    internal class BlockFlagConverter : JsonConverter<BlockFlagEnum>
    {
        private const string BLOCK_ID_FLAG_UNKNOWN = "BLOCK_ID_FLAG_UNKNOWN";
        private const string BLOCK_ID_FLAG_ABSENT = "BLOCK_ID_FLAG_ABSENT";
        private const string BLOCK_ID_FLAG_COMMIT = "BLOCK_ID_FLAG_COMMIT";
        private const string BLOCK_ID_FLAG_NIL = "BLOCK_ID_FLAG_NIL";

        public override BlockFlagEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var strValue = reader.GetString();
            if (strValue == null) throw new InvalidOperationException();
            return strValue switch
            {
                BLOCK_ID_FLAG_UNKNOWN => BlockFlagEnum.Unknown,
                BLOCK_ID_FLAG_ABSENT => BlockFlagEnum.Absent,
                BLOCK_ID_FLAG_COMMIT => BlockFlagEnum.Commit,
                BLOCK_ID_FLAG_NIL => BlockFlagEnum.Nil,
                _ => throw new NotSupportedException(),
            };
        }

        public override void Write(Utf8JsonWriter writer, BlockFlagEnum value, JsonSerializerOptions options)
        {
            switch (value)
            {
                case BlockFlagEnum.Unknown: writer.WriteStringValue(BLOCK_ID_FLAG_UNKNOWN); break;
                case BlockFlagEnum.Absent: writer.WriteStringValue(BLOCK_ID_FLAG_ABSENT); break;
                case BlockFlagEnum.Commit: writer.WriteStringValue(BLOCK_ID_FLAG_COMMIT); break;
                case BlockFlagEnum.Nil: writer.WriteStringValue(BLOCK_ID_FLAG_NIL); break;
                default: throw new NotSupportedException();
            }
        }
    }
}

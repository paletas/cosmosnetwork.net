using System.Text.Json;
using System.Text.Json.Serialization;

namespace CosmosNetwork.Serialization.Json.Converters
{
    internal class SignerModeConverter : JsonConverter<SignerModeEnum>
    {
        private const string SIGN_MODE_DIRECT = "SIGN_MODE_DIRECT";
        private const string SIGN_MODE_TEXTUAL = "SIGN_MODE_TEXTUAL";
        private const string SIGN_MODE_AMINO_LEGACY = "SIGN_MODE_LEGACY_AMINO_JSON";

        public override SignerModeEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var strValue = reader.GetString();
            if (strValue == null) throw new InvalidOperationException();
            return strValue switch
            {
                SIGN_MODE_DIRECT => SignerModeEnum.Direct,
                SIGN_MODE_TEXTUAL => SignerModeEnum.Textual,
                SIGN_MODE_AMINO_LEGACY => SignerModeEnum.AminoLegacy,
                _ => throw new NotSupportedException(),
            };
        }

        public override void Write(Utf8JsonWriter writer, SignerModeEnum value, JsonSerializerOptions options)
        {
            switch (value)
            {
                case SignerModeEnum.Direct: writer.WriteStringValue(SIGN_MODE_DIRECT); break;
                case SignerModeEnum.Textual: writer.WriteStringValue(SIGN_MODE_TEXTUAL); break;
                case SignerModeEnum.AminoLegacy: writer.WriteStringValue(SIGN_MODE_AMINO_LEGACY); break;
                default: throw new NotSupportedException();
            }
        }
    }
}

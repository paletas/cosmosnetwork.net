﻿using ProtoBuf.WellKnownTypes;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CosmosNetwork.Serialization.Json.Converters
{
    internal class TimestampConverter : JsonConverter<Timestamp>
    {
        public override Timestamp Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.String)
            {
                throw new JsonException();
            }

            string? value = reader.GetString();
            if (value is null)
            {
                throw new JsonException();
            }

            DateTime timestamp = DateTime.Parse(value);
            return new Timestamp(timestamp);
        }

        public override void Write(Utf8JsonWriter writer, Timestamp value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}

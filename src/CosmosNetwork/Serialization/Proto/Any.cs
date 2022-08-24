using ProtoBuf;
using System.Text.Json;

namespace CosmosNetwork.Serialization.Proto
{
    [ProtoContract]
    public record Any(
       [property: ProtoMember(1, Name = "type_url")] string? TypeUrl,
       [property: ProtoMember(2, Name = "value")] byte[] Value)
    {
        public static Any Pack<T>(T @object)
            where T : IHasAny
        {
            using MemoryStream memoryStream = new();
            JsonSerializer.Serialize(memoryStream, @object);

            return new Any(@object.TypeUrl, memoryStream.ToArray());
        }

        public static Any Pack(string? typeUrl, byte[] @object)
        {
            using MemoryStream memoryStream = new(@object);

            return new Any(typeUrl, memoryStream.ToArray());
        }

        public T? Unpack<T>()
            where T : IHasAny
        {
            return (T?)Unpack(typeof(T));
        }

        public object? Unpack(Type type)
        {
            using MemoryStream memoryStream = new(Value);
            return JsonSerializer.Deserialize(memoryStream, type);
        }

        public byte[] ToArray()
        {
            return this.Value;
        }
    }

    public interface IHasAny
    {
        string TypeUrl { get; }
    }
}
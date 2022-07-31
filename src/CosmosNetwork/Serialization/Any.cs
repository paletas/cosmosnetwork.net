using ProtoBuf;
using System.Text.Json;

namespace CosmosNetwork.Serialization
{
    [ProtoContract]
    internal record Any(
       [property: ProtoMember(1, Name = "type_url")] string TypeUrl,
       [property: ProtoMember(2, Name = "value")] byte[] Value)
    {
        public static Any Pack<T>(T @object)
            where T : IHasAny
        {
            using MemoryStream memoryStream = new();
            JsonSerializer.Serialize(memoryStream, @object);

            return new Any(@object.TypeUrl, memoryStream.ToArray());
        }

        public T? Unpack<T>()
            where T : IHasAny
        {
            using MemoryStream memoryStream = new(this.Value);
            return JsonSerializer.Deserialize<T>(memoryStream);
        }
    }

    internal interface IHasAny
    {
        string TypeUrl { get; }
    }
}
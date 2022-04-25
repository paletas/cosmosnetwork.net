namespace Terra.NET.API.Serialization.Json
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    internal class MessageDescriptorAttribute : Attribute
    {
        public string TerraType { get; set; }

        public string CosmosType { get; set; }
    }
}

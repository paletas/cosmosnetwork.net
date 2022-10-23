namespace CosmosNetwork
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class CosmosMessageAttribute : Attribute
    {
        public CosmosMessageAttribute(string cosmosType)
        {
            this.CosmosType = cosmosType;
        }

        public string CosmosType { get; set; }

        public string? CustomTypeAlias { get; set; }

    }
}

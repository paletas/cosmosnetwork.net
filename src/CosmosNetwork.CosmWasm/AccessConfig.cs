namespace CosmosNetwork.CosmWasm
{
    public record AccessConfig(
        AccessTypeEnum Permission,
        CosmosAddress? Address)
    {
        internal Serialization.AccessConfig ToSerialization()
        {
            return new Serialization.AccessConfig((Serialization.AccessTypeEnum)this.Permission, this.Address?.Address);
        }
    }

    public enum AccessTypeEnum
    {
        Unspecified = 0,
        Nobody = 1,
        OnlyAddress = 2,
        Everybody = 3,
    }
}
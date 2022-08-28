namespace CosmosNetwork.CosmWasm.Serialization
{
    internal record AccessConfig(
        AccessTypeEnum Permission,
        string? Address)
    {
        public CosmWasm.AccessConfig ToModel()
        {
            return new CosmWasm.AccessConfig((CosmWasm.AccessTypeEnum)this.Permission, this.Address);
        }
    }

    internal enum AccessTypeEnum
    {
        Unspecified = 0,
        Nobody = 1,
        OnlyAddress = 2,
        Everybody = 3,
    }
}
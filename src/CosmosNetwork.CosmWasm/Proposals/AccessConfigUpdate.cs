namespace CosmosNetwork.CosmWasm.Proposals
{
    public record AccessConfigUpdate(
        ulong CodeId,
        AccessConfig InstantiatePermission)
    {
        internal Serialization.Proposals.AccessConfigUpdate ToSerialization()
        {
            return new Serialization.Proposals.AccessConfigUpdate(
                this.CodeId,
                this.InstantiatePermission.ToSerialization());
        }
    }
}

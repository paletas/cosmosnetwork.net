namespace CosmosNetwork.CosmWasm.Serialization.Proposals
{
    internal record AccessConfigUpdate(
        ulong CodeId,
        AccessConfig InstantiatePermission)
    {
        public CosmWasm.Proposals.AccessConfigUpdate ToModel()
        {
            return new CosmWasm.Proposals.AccessConfigUpdate(
                this.CodeId,
                this.InstantiatePermission.ToModel());
        }
    }
}

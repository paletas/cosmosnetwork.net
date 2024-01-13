namespace CosmosNetwork.Modules.Slashing
{
    public record SigningInfo(
        CosmosAddress Address,
        int IndexOffset,
        DateTime? JailedUntil,
        int MissedBlocksCounter,
        int StartHeight,
        bool Tombstoned)
    {
        internal Serialization.SigningInfo ToSerialization()
        {
            return new Serialization.SigningInfo
            {
                Address = this.Address,
                IndexOffset = this.IndexOffset,
                JailedUntil = this.JailedUntil ?? DateTime.MinValue,
                MissedBlocksCounter = this.MissedBlocksCounter,
                StartHeight = this.StartHeight,
                Tombstoned = this.Tombstoned,
            };
        }
    }
}

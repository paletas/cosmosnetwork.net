namespace CosmosNetwork.Modules.Slashing
{
    public record SlashingParams(
        uint SignedBlocksWindow,
        decimal MinSignedPerWindow,
        TimeSpan DowntimeJailDuration,
        decimal SlashFractionDoubleSign,
        decimal SlashFractionDowntime)
    {
        internal Serialization.SlashingParams ToSerialization()
        {
            return new Serialization.SlashingParams
            {
                SignedBlocksWindow = this.SignedBlocksWindow,
                MinSignedPerWindow = this.MinSignedPerWindow,
                DowntimeJailDuration = this.DowntimeJailDuration,
                SlashFractionDoubleSign = this.SlashFractionDoubleSign,
                SlashFractionDowntime = this.SlashFractionDowntime,
            };
        }
    }
}

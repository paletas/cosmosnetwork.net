namespace CosmosNetwork.Modules.Slashing.Serialization
{
    public class SlashingParams
    {
        public uint SignedBlocksWindow { get; set; }

        public decimal MinSignedPerWindow { get; set; }

        public TimeSpan DowntimeJailDuration { get; set; }

        public decimal SlashFractionDoubleSign { get; set; }

        public decimal SlashFractionDowntime { get; set; }

        public CosmosNetwork.Modules.Slashing.SlashingParams ToModel()
        {
            return new CosmosNetwork.Modules.Slashing.SlashingParams(
                this.SignedBlocksWindow,
                this.MinSignedPerWindow,
                this.DowntimeJailDuration,
                this.SlashFractionDoubleSign,
                this.SlashFractionDowntime);
        }
    }
}

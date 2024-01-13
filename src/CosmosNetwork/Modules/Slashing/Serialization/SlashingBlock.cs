namespace CosmosNetwork.Modules.Slashing.Serialization
{
    public class SlashingBlock
    {
        public int Index { get; set; }

        public bool Missed { get; set; }

        public CosmosNetwork.Modules.Slashing.SlashingBlock ToModel()
        {
            return new CosmosNetwork.Modules.Slashing.SlashingBlock(this.Index, this.Missed);
        }
    }
}

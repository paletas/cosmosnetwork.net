namespace CosmosNetwork.Modules.Slashing
{
    public record SlashingBlock(int Index, bool Missed)
    {
        internal Serialization.SlashingBlock ToSerialization()
        {
            return new Serialization.SlashingBlock
            {
                Index = this.Index,
                Missed = this.Missed,
            };
        }
    }
}

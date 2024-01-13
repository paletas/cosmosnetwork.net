namespace CosmosNetwork.Modules.Slashing.Serialization
{
    public class SlashingValidatorBlocks
    {
        public string Address { get; set; }

        public SlashingBlock[] MissedBlocks { get; set; }
    }
}

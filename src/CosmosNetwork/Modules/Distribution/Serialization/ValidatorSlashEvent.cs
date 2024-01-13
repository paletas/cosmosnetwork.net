using System.Text.Json.Serialization;

namespace CosmosNetwork.Modules.Distribution.Serialization
{
    internal class ValidatorSlashEvent
    {
        public ulong Height { get; set; }

        public int Period { get; set; }

        public string ValidatorAddress { get; set; }

        [JsonPropertyName("validator_slash_event")]
        public SlashEvent SlashEvent { get; set; }

        public Distribution.SlashingEvent ToModel()
        {
            return new SlashingEvent(this.Height, this.Period, this.SlashEvent.Fraction);
        }
    }

    internal class SlashEvent
    {
        public decimal Fraction { get; set; }

        public int ValidatorPeriod { get; set; }
    }
}

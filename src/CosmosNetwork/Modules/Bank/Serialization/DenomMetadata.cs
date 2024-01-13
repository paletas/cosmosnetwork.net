using System.Text.Json.Serialization;

namespace CosmosNetwork.Modules.Bank.Serialization
{
    internal class DenomMetadata
    {
        [JsonPropertyName("base")]
        public string BaseSymbol { get; set; }

        public string Description { get; set; }

        public string Display { get; set; }

        public DenomUnit[] DenomUnits { get; set; }

        public Bank.DenomMetadata ToModel()
        {
            return new Bank.DenomMetadata(
                this.BaseSymbol,
                this.Description,
                this.Display,
                this.DenomUnits.Select(u => u.ToModel()).ToArray());
        }
    }

    internal class DenomUnit
    {
        public string Denom { get; set; }

        public int Exponent { get; set; }

        public string[] Aliases { get; set; }

        public Bank.DenomUnit ToModel()
        {
            return new Bank.DenomUnit(this.Denom, this.Exponent, this.Aliases);
        }
    }
}

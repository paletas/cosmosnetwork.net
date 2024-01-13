namespace CosmosNetwork.Modules.Bank
{
    public record DenomMetadata(string BaseSymbol, string Description, string DisplaySymbol, DenomUnit[] DenomUnits)
    {
        internal Serialization.DenomMetadata ToSerialization()
        {
            return new Serialization.DenomMetadata
            {
                BaseSymbol = this.BaseSymbol,
                Description = this.Description,
                Display = this.DisplaySymbol,
                DenomUnits = this.DenomUnits.Select(u => u.ToSerialization()).ToArray(),
            };
        }
    }

    public record DenomUnit(string Denom, int Exponent, string[] Aliases)
    {
        internal Serialization.DenomUnit ToSerialization()
        {
            return new Serialization.DenomUnit
            {
                Denom = this.Denom,
                Exponent = this.Exponent,
                Aliases = this.Aliases,
            };
        }
    }
}

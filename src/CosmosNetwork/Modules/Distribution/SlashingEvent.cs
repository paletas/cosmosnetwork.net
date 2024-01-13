namespace CosmosNetwork.Modules.Distribution
{
    public record SlashingEvent(ulong Height, int Period, decimal Fraction)
    {
        internal Serialization.ValidatorSlashEvent ToSerialization()
        {
            return new Serialization.ValidatorSlashEvent
            {
                Height = this.Height,
                Period = this.Period,
                SlashEvent = new Serialization.SlashEvent
                {
                    Fraction = this.Fraction,
                    ValidatorPeriod = this.Period
                }
            };
        }
    }
}

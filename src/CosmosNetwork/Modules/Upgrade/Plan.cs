namespace CosmosNetwork.Modules.Upgrade
{
    public record Plan(
        string Name,
        DateTime Time,
        long Height,
        string Info)
    {
        internal Serialization.Plan ToSerialization()
        {
            return new Serialization.Plan(this.Name, this.Time, this.Height, this.Info);
        }
    }
}

namespace CosmosNetwork.Modules.Upgrade.Serialization
{
    public record Plan(
        string Name,
        DateTime Time,
        long Height,
        string Info)
    {
        public Upgrade.Plan ToModel()
        {
            return new Upgrade.Plan(this.Name, this.Time, this.Height, this.Info);
        }
    }
}

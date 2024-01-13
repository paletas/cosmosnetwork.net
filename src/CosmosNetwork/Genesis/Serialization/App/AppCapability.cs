namespace CosmosNetwork.Genesis.Serialization.App
{
    internal class AppCapability
    {
        public int Index { get; set; }

        public Genesis.App.AppCapability ToModel()
        {
            return new Genesis.App.AppCapability(
                this.Index);
        }
    }
}

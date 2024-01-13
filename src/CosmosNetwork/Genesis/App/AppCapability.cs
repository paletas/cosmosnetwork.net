namespace CosmosNetwork.Genesis.App
{
    public record AppCapability(int Index)
    {
        internal Serialization.App.AppCapability ToSerialization()
        {
            return new Serialization.App.AppCapability
            {
                Index = this.Index
            };
        }
    }
}

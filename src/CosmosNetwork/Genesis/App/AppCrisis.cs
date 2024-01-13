namespace CosmosNetwork.Genesis.App
{
    public record AppCrisis(Coin ConstantFee)
    {
        internal Serialization.App.AppCrisis ToSerialization()
        {
            return new Serialization.App.AppCrisis
            {
                ConstantFee = this.ConstantFee.ToSerialization(),
            };
        }
    }
}

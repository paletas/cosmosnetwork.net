using CosmosNetwork.Serialization;

namespace CosmosNetwork.Genesis.Serialization.App
{
    internal class AppCrisis
    {
        public DenomAmount ConstantFee { get; set; }

        public Genesis.App.AppCrisis ToModel()
        {
            return new Genesis.App.AppCrisis(this.ConstantFee.ToModel());
        }
    }
}

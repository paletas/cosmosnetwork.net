using CosmosNetwork.Modules.Bank.Serialization;
using CosmosNetwork.Serialization;

namespace CosmosNetwork.Genesis.Serialization.App
{
    internal class AppBank
    {
        public BankBalance[] Balances { get; set; }

        public DenomMetadata[] DenomMetadata { get; set; }

        public BankParams Params { get; set; }

        public DenomAmount[] Supply { get; set; }

        public Genesis.App.AppBank ToModel()
        {
            return new Genesis.App.AppBank(
                this.Balances.Select(acc => acc.ToModel()).ToArray(),
                this.DenomMetadata.Select(acc => acc.ToModel()).ToArray(),
                this.Params.ToModel(),
                this.Supply.Select(acc => acc.ToModel()).ToArray());
        }
    }
}

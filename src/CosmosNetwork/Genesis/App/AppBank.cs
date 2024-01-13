using CosmosNetwork.Modules.Bank;

namespace CosmosNetwork.Genesis.App
{
    public record AppBank(
        BankBalance[] Balances,
        DenomMetadata[] DenomMetadata,
        BankParams Params,
        Coin[] Supply)
    {
        internal Serialization.App.AppBank ToSerialization()
        {
            return new Serialization.App.AppBank
            {
                Balances = this.Balances.Select(acc => acc.ToSerialization()).ToArray(),
                DenomMetadata = this.DenomMetadata.Select(acc => acc.ToSerialization()).ToArray(),
                Params = this.Params.ToSerialization(),
                Supply = this.Supply.Select(acc => acc.ToSerialization()).ToArray()
            };
        }
    }
}

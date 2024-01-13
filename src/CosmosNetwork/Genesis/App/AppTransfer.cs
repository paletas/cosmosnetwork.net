using CosmosNetwork.Modules.Transfer;

namespace CosmosNetwork.Genesis.App
{
    public record AppTransfer(
        TransferParams Params,
        Coin[] DenomTraces,
        string PortId)
    {
        internal Serialization.App.AppTransfer ToSerialization()
        {
            return new Serialization.App.AppTransfer
            {
                Params = this.Params.ToSerialization(),
                DenomTraces = this.DenomTraces.Select(d => d.ToSerialization()).ToArray(),
                PortId = PortId
            };
        }
    }
}

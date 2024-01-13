using CosmosNetwork.Modules.Transfer.Serialization;
using CosmosNetwork.Serialization;

namespace CosmosNetwork.Genesis.Serialization.App
{
    internal class AppTransfer
    {
        public TransferParams Params { get; set; }

        public DenomAmount[] DenomTraces { get; set; }

        public string PortId { get; set; }

        public Genesis.App.AppTransfer ToModel()
        {
            return new Genesis.App.AppTransfer(
                this.Params.ToModel(),
                this.DenomTraces.Select(d => d.ToModel()).ToArray(),
                this.PortId);
        }
    }
}

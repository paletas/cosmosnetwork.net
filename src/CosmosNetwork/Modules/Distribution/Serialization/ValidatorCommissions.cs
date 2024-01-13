using CosmosNetwork.Serialization;

namespace CosmosNetwork.Modules.Distribution.Serialization
{
    internal class ValidatorCommissions
    {
        public string ValidatorAddress { get; set; }

        public ValidatorAccumulated Accumulated { get; set; }
    }

    internal class ValidatorAccumulated
    {
        public DenomAmount[] Commission { get; set; }
    }
}

using CosmosNetwork.Serialization;

namespace CosmosNetwork.Modules.Distribution.Serialization
{
    internal class ValidatorOutstandingRewards
    {
        public string ValidatorAddress { get; set; }

        public DenomAmount[] OutstandingRewards { get; set; }
    }
}

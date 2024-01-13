using CosmosNetwork.Serialization;

namespace CosmosNetwork.Modules.Distribution.Serialization
{
    internal class FeePool
    {
        public DenomAmount[] CommunityPool { get; set; }

        public Modules.Distribution.FeePool ToModel()
        {
            return new Modules.Distribution.FeePool(
                this.CommunityPool.Select(acc => acc.ToDecimalModel()).ToArray());
        }
    }
}

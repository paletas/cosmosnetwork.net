using CosmosNetwork.Serialization.Proto;

namespace CosmosNetwork.Modules.FeeGrant.Serialization.Allowances
{
    public interface IAllowance : IHasAny
    {
        FeeGrant.Allowances.IAllowance ToModel();
    }
}

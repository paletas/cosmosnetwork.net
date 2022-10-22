using CosmosNetwork.Serialization.Proto;
using System.Text.Json.Serialization;

namespace CosmosNetwork.Modules.FeeGrant.Serialization.Allowances
{
    [JsonConverter(typeof(IAllowance))]
    public interface IAllowance : IHasAny
    {
        FeeGrant.Allowances.IAllowance ToModel();
    }
}

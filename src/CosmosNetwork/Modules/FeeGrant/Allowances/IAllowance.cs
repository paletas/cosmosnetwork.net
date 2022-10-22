namespace CosmosNetwork.Modules.FeeGrant.Allowances
{
    public interface IAllowance
    {
        Serialization.Allowances.IAllowance ToSerialization();
    }
}
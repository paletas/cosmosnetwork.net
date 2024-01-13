namespace CosmosNetwork.Modules.Staking
{
    public record Validator(
        CosmosAddress Operator,
        IKey ConsensusPublicKey,
        bool Jailed,
        BondStatusEnum Status,
        ulong Tokens,
        decimal DelegatorShares,
        ValidatorDescription Description,
        ulong? UnbondingHeight,
        DateTime? UnbondingTime,
        ValidatorCommission Commission,
        decimal MinimumSelfDelegation,
        decimal Power);

    public record ValidatorDescription(string Moniker, string Identity, string Details, string Website, string SecurityContact);

    public record ValidatorCommission(ValidatorCommissionRates CommissionRates, DateTime UpdateTime);

    public record ValidatorCommissionRates(decimal Rate, decimal MaxRate, decimal MaxRateChange);

    public record ConsensusPublicKey(string TypeUrl, string Value);

    public enum BondStatusEnum
    {
        Unspecified = Serialization.BondStatusEnum.Unspecified,
        Unbonded = Serialization.BondStatusEnum.Unbonded,
        Unbonding = Serialization.BondStatusEnum.Unbonding,
        Bonded = Serialization.BondStatusEnum.Bonded
    }
}

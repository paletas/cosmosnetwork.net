using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace CosmosNetwork.Modules.Staking.Serialization
{
    internal record Validator(
        string OperatorAddress,
        [property: JsonPropertyName("consensus_pubkey")] CosmosNetwork.Serialization.Json.PublicKey ConsensusPublicKey,
        bool Jailed,
        BondStatusEnum Status,
        ulong Tokens,
        decimal DelegatorShares,
        ValidatorDescription Description,
        ulong? UnbondingHeight,
        DateTime? UnbondingTime,
        ValidatorCommission Commission,
        [property: JsonPropertyName("min_self_delegation")] decimal MinimumSelfDelegation)
    {
        public Staking.Validator ToModel()
        {
            return new Staking.Validator(
                OperatorAddress,
                ConsensusPublicKey.ToModel(),
                Jailed,
                (Staking.BondStatusEnum)Status,
                Tokens,
                DelegatorShares,
                Description.ToModel(),
                UnbondingHeight,
                UnbondingTime,
                Commission.ToModel(),
                MinimumSelfDelegation);
        }
    }

    internal record ValidatorDescription(string Moniker, string Identity, string Details, string Website, string SecurityContact)
    {
        public Staking.ValidatorDescription ToModel()
        {
            return new Staking.ValidatorDescription(Moniker, Identity, Details, Website, SecurityContact);
        }
    }

    internal record ValidatorCommission(ValidatorCommissionRates CommissionRates, DateTime UpdateTime)
    {
        public Staking.ValidatorCommission ToModel()
        {
            return new Staking.ValidatorCommission(CommissionRates.ToModel(), UpdateTime);
        }
    }

    internal record ValidatorCommissionRates(decimal Rate, decimal MaxRate, decimal MaxRateChange)
    {
        public Staking.ValidatorCommissionRates ToModel()
        {
            return new Staking.ValidatorCommissionRates(Rate, MaxRate, MaxRateChange);
        }
    }

    [JsonConverter(typeof(JsonStringEnumMemberConverter))]
    internal enum BondStatusEnum
    {
        [EnumMember(Value = "BOND_STATUS_UNSPECIFIED")]
        Unspecified = 0,

        [EnumMember(Value = "BOND_STATUS_UNBONDED")]
        Unbonded = 1,

        [EnumMember(Value = "BOND_STATUS_UNBONDING")]
        Unbonding = 2,

        [EnumMember(Value = "BOND_STATUS_BONDED")]
        Bonded = 3
    }
}

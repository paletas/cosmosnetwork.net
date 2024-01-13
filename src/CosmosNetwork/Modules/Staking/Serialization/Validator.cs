using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace CosmosNetwork.Modules.Staking.Serialization
{
    public record Validator(
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
        public Staking.Validator ToModel(ValidatorPower? validatorPower)
        {
            return new Staking.Validator(
                this.OperatorAddress,
                this.ConsensusPublicKey.ToModel(),
                this.Jailed,
                (Staking.BondStatusEnum)this.Status,
                this.Tokens,
                this.DelegatorShares,
                this.Description.ToModel(),
                this.UnbondingHeight,
                this.UnbondingTime,
                this.Commission.ToModel(),
                this.MinimumSelfDelegation,
                validatorPower?.Power ?? default);
        }
    }

    public record ValidatorDescription(string Moniker, string Identity, string Details, string Website, string SecurityContact)
    {
        public Staking.ValidatorDescription ToModel()
        {
            return new Staking.ValidatorDescription(this.Moniker, this.Identity, this.Details, this.Website, this.SecurityContact);
        }
    }

    public record ValidatorCommission(ValidatorCommissionRates CommissionRates, DateTime UpdateTime)
    {
        public Staking.ValidatorCommission ToModel()
        {
            return new Staking.ValidatorCommission(this.CommissionRates.ToModel(), this.UpdateTime);
        }
    }

    public record ValidatorCommissionRates(decimal Rate, decimal MaxRate, decimal MaxRateChange)
    {
        public Staking.ValidatorCommissionRates ToModel()
        {
            return new Staking.ValidatorCommissionRates(this.Rate, this.MaxRate, this.MaxRateChange);
        }
    }

    [JsonConverter(typeof(JsonStringEnumMemberConverter))]
    public enum BondStatusEnum
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

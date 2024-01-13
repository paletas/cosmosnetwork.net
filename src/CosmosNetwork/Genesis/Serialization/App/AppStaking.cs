using CosmosNetwork.Modules.Staking.Serialization;

namespace CosmosNetwork.Genesis.Serialization.App
{
    public class AppStaking
    {
        public Delegation[] Delegations { get; set; }

        public bool Exported { get; set; }

        public ulong LastTotalPower { get; set; }

        public ValidatorPower[] LastValidatorPowers { get; set; }

        public StakingParams Params { get; set; }

        public Redelegation[] Redelegations { get; set; }

        public Validator[] Validators { get; set; }

        public Genesis.App.AppStaking ToModel()
        {
            return new Genesis.App.AppStaking(
                this.Delegations.Select(d => d.ToModel()).ToArray(),
                this.Redelegations.Select(r => r.ToModel()).ToArray(),
                this.Validators.Select(v => v.ToModel(this.LastValidatorPowers.SingleOrDefault(lp => lp.Address == v.OperatorAddress))).ToArray(),
                this.Params.ToModel(),
                this.LastTotalPower,
                this.Exported);
        }
    }
}

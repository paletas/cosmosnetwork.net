using CosmosNetwork.Modules.Distribution.Serialization;
using System.Text.Json.Serialization;

namespace CosmosNetwork.Genesis.Serialization.App
{
    internal class AppDistribution
    {
        public Delegator[] DelegatorStartingInfos { get; set; }

        public DelegatorWithdraw[] DelegatorWithdrawInfos { get; set; }

        public FeePool FeePool { get; set; }

        public ValidatorOutstandingRewards[] OutstandingRewards { get; set; }

        public DistributionParams Params { get; set; }

        [JsonPropertyName("previous_proposer")]
        public string PreviousProposerAddress { get; set; }

        [JsonPropertyName("validator_accumulated_commissions")]
        public ValidatorCommissions[] AccumulatedComissions { get; set; }

        [JsonPropertyName("validator_current_rewards")]
        public ValidatorCurrentRewards[] CurrentRewards { get; set; }

        [JsonPropertyName("validator_historical_rewards")]
        public ValidatorHistoricalRewards[] HistoricalRewards { get; set; }

        [JsonPropertyName("validator_slash_events")]
        public ValidatorSlashEvent[] SlashEvents { get; set; }

        public Genesis.App.AppDistribution ToModel()
        {
            Dictionary<string, IntermediateValidatorModel> validators = GatherValidatorData();

            return new Genesis.App.AppDistribution(
                this.DelegatorStartingInfos.Select(d => d.ToModel(this.DelegatorWithdrawInfos.SingleOrDefault(w => w.DelegatorAddress == d.DelegatorAddress))).ToArray(),
                this.FeePool.ToModel(),
                validators.Values.Select(v => v.ToModel()).ToArray(),
                this.Params.ToModel(),
                this.PreviousProposerAddress);
        }

        private Dictionary<string, IntermediateValidatorModel> GatherValidatorData()
        {
            Dictionary<string, IntermediateValidatorModel> validators = [];

            foreach (ValidatorOutstandingRewards outstandingRewards in this.OutstandingRewards)
            {
                if (validators.TryGetValue(outstandingRewards.ValidatorAddress, out IntermediateValidatorModel? validatorModel))
                {
                    validatorModel.OutstandingRewards = outstandingRewards;
                }
                else
                {
                    validators.Add(outstandingRewards.ValidatorAddress, new IntermediateValidatorModel { OutstandingRewards = outstandingRewards });
                }
            }

            foreach (ValidatorCommissions accumulatedCommissions in this.AccumulatedComissions)
            {
                if (validators.TryGetValue(accumulatedCommissions.ValidatorAddress, out IntermediateValidatorModel? validatorModel))
                {
                    validatorModel.AccumulatedCommissions = accumulatedCommissions;
                }
                else
                {
                    validators.Add(accumulatedCommissions.ValidatorAddress, new IntermediateValidatorModel { AccumulatedCommissions = accumulatedCommissions });
                }
            }

            foreach (ValidatorCurrentRewards currentRewards in this.CurrentRewards)
            {
                if (validators.TryGetValue(currentRewards.ValidatorAddress, out IntermediateValidatorModel? validatorModel))
                {
                    validatorModel.CurrentRewards = currentRewards;
                }
                else
                {
                    validators.Add(currentRewards.ValidatorAddress, new IntermediateValidatorModel { CurrentRewards = currentRewards });
                }
            }

            foreach (ValidatorHistoricalRewards historicalRewards in this.HistoricalRewards)
            {
                if (validators.TryGetValue(historicalRewards.ValidatorAddress, out IntermediateValidatorModel? validatorModel))
                {
                    validatorModel.HistoricalRewards.Add(historicalRewards);
                }
                else
                {
                    validators.Add(historicalRewards.ValidatorAddress, new IntermediateValidatorModel { HistoricalRewards = new List<ValidatorHistoricalRewards> { historicalRewards } });
                }
            }

            foreach (ValidatorSlashEvent slashEvent in this.SlashEvents)
            {
                if (validators.TryGetValue(slashEvent.ValidatorAddress, out IntermediateValidatorModel? validatorModel))
                {
                    validatorModel.SlashEvents.Add(slashEvent);
                }
                else
                {
                    validators.Add(slashEvent.ValidatorAddress, new IntermediateValidatorModel { SlashEvents = new List<ValidatorSlashEvent> { slashEvent } });
                }
            }

            return validators;
        }

        private class IntermediateValidatorModel
        {
            public string ValidatorAddress { get; set; }

            public ValidatorOutstandingRewards OutstandingRewards { get; set; }

            public ValidatorCommissions AccumulatedCommissions { get; set; }

            public ValidatorCurrentRewards CurrentRewards { get; set; }

            public List<ValidatorHistoricalRewards> HistoricalRewards { get; set; } = [];

            public List<ValidatorSlashEvent> SlashEvents { get; set; } = [];

            public Modules.Distribution.Validator ToModel()
            {
                return new Modules.Distribution.Validator(
                    this.ValidatorAddress,
                    this.AccumulatedCommissions.Accumulated.Commission.Select(c => c.ToDecimalModel()).ToArray(),
                    this.CurrentRewards.Rewards.Period,
                    this.CurrentRewards.Rewards.Rewards.Select(c => c.ToDecimalModel()).ToArray(),
                    this.HistoricalRewards.Select(hr => hr.ToModel()).ToArray(),
                    this.OutstandingRewards.OutstandingRewards.Select(c => c.ToDecimalModel()).ToArray(),
                    this.SlashEvents.Select(e => e.ToModel()).ToArray());
            }
        }
    }
}

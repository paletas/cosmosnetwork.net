using System.Text.Json.Serialization;
using CosmosNetwork.Modules.Slashing.Serialization;

namespace CosmosNetwork.Genesis.Serialization.App
{
    public class AppSlashing
    {
        [JsonPropertyName("missed_blocks")]
        public SlashingValidatorBlocks[] Validators { get; set; }

        public SlashingParams Params { get; set; }

        public SlashingValidatorSigningInfo[] SigningInfos { get; set; }

        public CosmosNetwork.Genesis.App.AppSlashing ToModel()
        {
            Dictionary<string, IntermediateValidatorModel> validators = [];

            foreach (SlashingValidatorBlocks validator in this.Validators)
            {
                if (validators.TryGetValue(validator.Address, out IntermediateValidatorModel? validatorModel))
                {
                    validatorModel.Blocks = validator;
                }
                else
                {
                    validators[validator.Address] = new IntermediateValidatorModel
                    {
                        ValidatorAddress = validator.Address,
                        Blocks = validator
                    };
                }
            }

            foreach (SlashingValidatorSigningInfo signingInfo in this.SigningInfos)
            {
                if (validators.TryGetValue(signingInfo.Address, out IntermediateValidatorModel? validatorModel))
                {
                    validatorModel.SigningInfo = signingInfo;
                }
                else
                {
                    validators[signingInfo.Address] = new IntermediateValidatorModel
                    {
                        ValidatorAddress = signingInfo.Address,
                        SigningInfo = signingInfo
                    };
                }
            }

            return new Genesis.App.AppSlashing(validators.Values.Select(v => v.ToModel()).ToArray(), this.Params.ToModel());
        }

        private class IntermediateValidatorModel
        {
            public string ValidatorAddress { get; set; }

            public SlashingValidatorBlocks Blocks { get; set; }

            public SlashingValidatorSigningInfo SigningInfo { get; set; }

            public Modules.Slashing.SlashingValidator ToModel()
            {
                return new Modules.Slashing.SlashingValidator(
                    this.ValidatorAddress,
                    this.SigningInfo.SigningInfo.ToModel(),
                    this.Blocks.MissedBlocks.Select(b => b.ToModel()).ToArray());
            }
        }
    }
}

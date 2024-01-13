using CosmosNetwork.Genesis.Serialization.App;
using System.Text.Json.Serialization;

namespace CosmosNetwork.Genesis.Serialization
{
    internal class GenesisFile
    {
        public DateTime GenesisTime { get; set; }

        public string ChainId { get; set; }

        public ConsensusParams ConsensusParams { get; set; }

        public AppState AppState { get; set; }

        public ulong InitialHeight { get; set; }

        public GenesisValidator[] Validators { get; set; }

        public string AppHash { get; set; }

        public Genesis.GenesisFile ToModel()
        {
            return new Genesis.GenesisFile(
                this.GenesisTime,
                this.ChainId,
                this.InitialHeight,
                this.ConsensusParams.ToModel(),
                this.AppHash,
                this.AppState.ToModel());
        }
    }

    internal class AppState
    {
        public AppAuth Auth { get; set; }

        public AppBank Bank { get; set; }

        public AppCapability Capability { get; set; }

        public AppCrisis Crisis { get; set; }

        public AppDistribution Distribution { get; set; }

        [JsonPropertyName("genutil")]
        public AppGenUtil GenUtil { get; set; }

        public AppGov Gov { get; set; }

        public AppMint Mint { get; set; }

        public AppSlashing Slashing { get; set; }

        public AppStaking Staking { get; set; }

        public AppTransfer Transfer { get; set; }

        public State ToModel()
        {
            return new State(
                this.Auth.ToModel(),
                this.Bank.ToModel(),
                this.Capability.ToModel(),
                this.Crisis.ToModel(),
                this.Distribution.ToModel(),
                this.GenUtil.ToModel(),
                this.Gov.ToModel(),
                this.Mint.ToModel(),
                this.Slashing.ToModel(),
                this.Staking.ToModel(),
                this.Transfer.ToModel());
        }
    }
}

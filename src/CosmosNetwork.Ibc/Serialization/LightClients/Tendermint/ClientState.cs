using CosmosNetwork.Confio.Serialization;
using CosmosNetwork.Ibc.Serialization.Core.Client;
using ProtoBuf.WellKnownTypes;

namespace CosmosNetwork.Ibc.Serialization.LightClients.Tendermint
{
    internal record ClientState(
        string ChainId,
        Fraction TrustLevel,
        Duration TrustingPeriod,
        Duration UnbondingPeriod,
        Duration MaxClockDrift,
        Height FrozenHeight,
        Height LatestHeight,
        ProofSpec[] ProofSpecs,
        string[] UpgradePath,
        bool AllowUpdateAfterExpiry,
        bool AllowUpdateAfterMisbehaviour) : IClientState
    {
        public const string AnyType = "/ibc.lightclients.tendermint.v1.ClientState";

        public string TypeUrl => AnyType;

        public Ibc.LightClients.IClientState ToModel()
        {
            return new Ibc.LightClients.Tendermint.ClientState(
                this.ChainId,
                this.TrustLevel.ToModel(),
                this.TrustingPeriod.AsTimeSpan(),
                this.UnbondingPeriod.AsTimeSpan(),
                this.MaxClockDrift.AsTimeSpan(),
                this.FrozenHeight.ToModel(),
                this.LatestHeight.ToModel(),
                this.ProofSpecs.Select(ps => ps.ToModel()).ToArray(),
                this.UpgradePath,
                this.AllowUpdateAfterExpiry,
                this.AllowUpdateAfterMisbehaviour);
        }
    }
}

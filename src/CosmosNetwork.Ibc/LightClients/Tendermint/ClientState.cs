using CosmosNetwork.Confio.Types;
using CosmosNetwork.Ibc.Core.Client;
using ProtoBuf.WellKnownTypes;

namespace CosmosNetwork.Ibc.LightClients.Tendermint
{
    public record ClientState(
        string ChainId,
        Fraction TrustLevel,
        TimeSpan TrustingPeriod,
        TimeSpan UnbondingPeriod,
        TimeSpan MaxClockDrift,
        Height FrozenHeight,
        Height LatestHeight,
        ProofSpec[] ProofSpecs,
        string[] UpgradePath,
        bool AllowUpdateAfterExpiry,
        bool AllowUpdateAfterMisbehaviour) : IClientState
    {
        public Serialization.LightClients.IClientState ToSerialization()
        {
            return new Serialization.LightClients.Tendermint.ClientState(
                this.ChainId,
                this.TrustLevel.ToSerialization(),
                new Duration(this.TrustingPeriod),
                new Duration(this.UnbondingPeriod),
                new Duration(this.MaxClockDrift),
                this.FrozenHeight.ToSerialization(),
                this.LatestHeight.ToSerialization(),
                this.ProofSpecs.Select(ps => ps.ToSerialization()).ToArray(),
                this.UpgradePath,
                this.AllowUpdateAfterExpiry,
                this.AllowUpdateAfterMisbehaviour);
        }
    }
}

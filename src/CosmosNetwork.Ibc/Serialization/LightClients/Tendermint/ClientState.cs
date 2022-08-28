using CosmosNetwork.Confio.Serialization;
using CosmosNetwork.Ibc.Serialization.Core.Client;
using CosmosNetwork.Ibc.Serialization.Json;
using ProtoBuf;
using ProtoBuf.WellKnownTypes;
using System.Text.Json.Serialization;

namespace CosmosNetwork.Ibc.Serialization.LightClients.Tendermint
{
    [ProtoContract]
    internal record ClientState(
        [property: ProtoMember(1, Name = "chain_id")] string ChainId,
        [property: ProtoMember(2, Name = "trust_level")] Fraction TrustLevel,
        [property: ProtoMember(3, Name = "trusting_period")] Duration TrustingPeriod,
        [property: ProtoMember(4, Name = "unbonding_period")] Duration UnbondingPeriod,
        [property: ProtoMember(5, Name = "max_clock_drift")] Duration MaxClockDrift,
        [property: ProtoMember(6, Name = "frozen_height")] Height FrozenHeight,
        [property: ProtoMember(7, Name = "latest_height")] Height LatestHeight,
        [property: ProtoMember(8, Name = "proof_specs")] ProofSpec[] ProofSpecs,
        [property: ProtoMember(9, Name = "upgrade_path")] string[] UpgradePath,
        [property: ProtoMember(10, Name = "allow_update_after_expiry")] bool AllowUpdateAfterExpiry,
        [property: ProtoMember(11, Name = "allow_update_after_misbehaviour")] bool AllowUpdateAfterMisbehaviour) : IClientState
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

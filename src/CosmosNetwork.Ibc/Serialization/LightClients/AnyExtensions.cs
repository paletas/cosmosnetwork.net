using CosmosNetwork.Serialization.Proto;

namespace CosmosNetwork.Ibc.Serialization.LightClients
{
    internal static class AnyExtensions
    {
        public static IClientState UnpackClientState(this Any clientState)
        {
            return clientState.TypeUrl switch
            {
                Localhost.ClientState.AnyType => clientState.Unpack<Localhost.ClientState>() ?? throw new InvalidOperationException(),
                SoloMachine.ClientState.AnyType => clientState.Unpack<SoloMachine.ClientState>() ?? throw new InvalidOperationException(),
                SoloMachine.ClientStateV1.AnyType => clientState.Unpack<SoloMachine.ClientStateV1>() ?? throw new InvalidOperationException(),
                Tendermint.ClientState.AnyType => clientState.Unpack<Tendermint.ClientState>() ?? throw new InvalidOperationException(),
                _ => throw new NotSupportedException(),
            };
        }

        public static IConsensusState UnpackConsensusState(this Any consensusState)
        {
            return consensusState.TypeUrl switch
            {
                SoloMachine.ConsensusState.AnyType => consensusState.Unpack<SoloMachine.ConsensusState>() ?? throw new InvalidOperationException(),
                SoloMachine.ConsensusStateV1.AnyType => consensusState.Unpack<SoloMachine.ConsensusStateV1>() ?? throw new InvalidOperationException(),
                Tendermint.ConsensusState.AnyType => consensusState.Unpack<Tendermint.ConsensusState>() ?? throw new InvalidOperationException(),
                _ => throw new NotSupportedException(),
            };
        }

        public static IHeader UnpackHeader(this Any header)
        {
            return header.TypeUrl switch
            {
                SoloMachine.Header.AnyType => header.Unpack<SoloMachine.Header>() ?? throw new InvalidOperationException(),
                SoloMachine.HeaderV1.AnyType => header.Unpack<SoloMachine.HeaderV1>() ?? throw new InvalidOperationException(),
                Tendermint.Header.AnyType => header.Unpack<Tendermint.Header>() ?? throw new InvalidOperationException(),
                _ => throw new NotSupportedException(),
            };
        }

        public static IMisbehaviour UnpackMisbehaviour(this Any misbehaviour)
        {
            return misbehaviour.TypeUrl switch
            {
                SoloMachine.Misbehaviour.AnyType => misbehaviour.Unpack<SoloMachine.Misbehaviour>() ?? throw new InvalidOperationException(),
                SoloMachine.MisbehaviourV1.AnyType => misbehaviour.Unpack<SoloMachine.MisbehaviourV1>() ?? throw new InvalidOperationException(),
                Tendermint.Misbehaviour.AnyType => misbehaviour.Unpack<Tendermint.Misbehaviour>() ?? throw new InvalidOperationException(),
                _ => throw new NotSupportedException(),
            };
        }
    }
}

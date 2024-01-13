namespace CosmosNetwork.Genesis
{
    public record GenesisFile(
        DateTime GenesisTime,
        string ChainId,
        ulong InitialHeight,
        ConsensusParams Consensus,
        string AppHash,
        State AppState)
    {

    }
}

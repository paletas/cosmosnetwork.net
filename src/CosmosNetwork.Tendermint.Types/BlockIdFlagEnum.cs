namespace CosmosNetwork.Tendermint.Types
{
    public enum BlockIdFlagEnum
    {
        Unknown = Serialization.BlockIdFlagEnum.Unknown,

        Absent = Serialization.BlockIdFlagEnum.Absent,

        Commit = Serialization.BlockIdFlagEnum.Commit,

        Nil = Serialization.BlockIdFlagEnum.Nil
    }
}

namespace CosmosNetwork.Ibc.Core.Channel
{
    public enum StateEnum
    {
        Unitilialized = 0,
        Unspecified = 0,

        Init = 1,
        TryOpen = 2,
        Open = 3,
        Closed = 4
    }
}

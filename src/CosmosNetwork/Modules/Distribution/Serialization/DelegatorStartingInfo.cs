namespace CosmosNetwork.Modules.Distribution.Serialization
{
    internal class DelegatorStartingInfo
    {
        public ulong Height { get; set; }

        public int PreviousPeriod { get; set; }

        public decimal Stake { get; set; }
    }
}

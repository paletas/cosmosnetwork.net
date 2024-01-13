namespace CosmosNetwork.Modules.Slashing.Serialization
{
    public class SigningInfo
    {
        public string Address { get; set; }

        public int IndexOffset { get; set; }

        public DateTime JailedUntil { get; set; }

        public int MissedBlocksCounter { get; set; }

        public int StartHeight { get; set; }

        public bool Tombstoned { get; set; }

        public Slashing.SigningInfo ToModel()
        {
            return new Slashing.SigningInfo(
                Address: this.Address,
                IndexOffset: this.IndexOffset,
                JailedUntil: this.JailedUntil,
                MissedBlocksCounter: this.MissedBlocksCounter,
                StartHeight: this.StartHeight,
                Tombstoned: this.Tombstoned);
        }
    }
}

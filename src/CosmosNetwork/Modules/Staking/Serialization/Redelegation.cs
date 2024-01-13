namespace CosmosNetwork.Modules.Staking.Serialization
{
    public class Redelegation
    {
        public string DelegatorAddress { get; set; }

        public string ValidatorSrcAddress { get; set; }

        public string ValidatorDstAddress { get; set; }

        public RedelegationEntry[] Entries { get; set; }

        public Staking.Redelegation ToModel()
        {
            return new Staking.Redelegation(
                this.DelegatorAddress,
                this.ValidatorSrcAddress,
                this.ValidatorDstAddress,
                this.Entries.Select(x => x.ToModel()).ToArray());
        }
    }

    public class RedelegationEntry
    {
        public decimal InitialBalance { get; set; }

        public decimal SharesDst { get; set; }

        public ulong CreationHeight { get; set; }

        public DateTime CompletionTime { get; set; }

        public Staking.RedelegationEntry ToModel()
        {
            return new Staking.RedelegationEntry(
                this.CreationHeight,
                this.CompletionTime,
                this.InitialBalance,
                this.SharesDst);
        }
    }
}

using ProtoBuf.WellKnownTypes;

namespace CosmosNetwork.Modules.Auth.Serialization.Accounts
{
    internal class ContinuousVestingAccount : Account
    {
        public BaseVestingAccount BaseVestingAccount { get; set; }

        public Timestamp StartTime { get; set; }

        public override Auth.ContinuousVestingAccount ToModel()
        {
            return new Auth.ContinuousVestingAccount(
                this.BaseVestingAccount.BaseAccount.Address,
                this.BaseVestingAccount.BaseAccount.Sequence,
                this.BaseVestingAccount.BaseAccount.AccountNumber,
                this.BaseVestingAccount.OriginalVesting.Select(c => c.ToModel()).ToArray(),
                this.BaseVestingAccount.DelegatedFree.Select(c => c.ToModel()).ToArray(),
                this.BaseVestingAccount.DelegatedVesting.Select(c => c.ToModel()).ToArray(),
                this.StartTime,
                this.BaseVestingAccount.EndTime);
        }
    }
}

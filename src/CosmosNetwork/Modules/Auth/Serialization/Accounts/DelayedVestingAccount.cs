using CosmosNetwork.Serialization;
using ProtoBuf.WellKnownTypes;

namespace CosmosNetwork.Modules.Auth.Serialization.Accounts
{
    internal class DelayedVestingAccount : Account
    {
        public BaseVestingAccount BaseVestingAccount { get; set; }

        public override Auth.DelayedVestingAccount ToModel()
        {
            return new Auth.DelayedVestingAccount(
                this.BaseVestingAccount.BaseAccount.Address,
                this.BaseVestingAccount.BaseAccount.Sequence,
                this.BaseVestingAccount.BaseAccount.AccountNumber,
                this.BaseVestingAccount.OriginalVesting.Select(c => c.ToModel()).ToArray(),
                this.BaseVestingAccount.DelegatedFree.Select(c => c.ToModel()).ToArray(),
                this.BaseVestingAccount.DelegatedVesting.Select(c => c.ToModel()).ToArray(),
                this.BaseVestingAccount.StartTime,
                this.BaseVestingAccount.EndTime);
        }
    }

    internal class BaseVestingAccount
    {
        public BaseAccount BaseAccount { get; set; }

        public DenomAmount[] OriginalVesting { get; set; } = [];

        public DenomAmount[] DelegatedFree { get; set; } = [];

        public DenomAmount[] DelegatedVesting { get; set; } = [];

        public Timestamp? StartTime { get; set; }

        public Timestamp? EndTime { get; set; }
    }
}

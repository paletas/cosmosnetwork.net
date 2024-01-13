using CosmosNetwork.Serialization;
using ProtoBuf.WellKnownTypes;

namespace CosmosNetwork.Modules.Auth.Serialization.Accounts
{
    internal class PeriodicVestingAccount : Account
    {
        public BaseVestingAccount BaseVestingAccount { get; set; }

        public Timestamp StartTime { get; set; }

        public VestingPeriod[] VestingPeriods { get; set; }

        public override Auth.PeriodicVestingAccount ToModel()
        {
            return new Auth.PeriodicVestingAccount(
                this.BaseVestingAccount.BaseAccount.Address,
                this.BaseVestingAccount.BaseAccount.Sequence,
                this.BaseVestingAccount.BaseAccount.AccountNumber,
                this.BaseVestingAccount.OriginalVesting.Select(c => c.ToModel()).ToArray(),
                this.BaseVestingAccount.DelegatedFree.Select(c => c.ToModel()).ToArray(),
                this.BaseVestingAccount.DelegatedVesting.Select(c => c.ToModel()).ToArray(),
                this.StartTime,
                this.BaseVestingAccount.EndTime,
                this.VestingPeriods.Select(c => c.ToModel()).ToArray());
        }
    }

    internal class VestingPeriod
    {
        public ulong Length { get; set; }

        public DenomAmount[] Amount { get; set; }

        public Auth.VestingPeriod ToModel()
        {
            return new Auth.VestingPeriod(
                this.Length,
                this.Amount.Select(c => c.ToModel()).ToArray());
        }
    }
}

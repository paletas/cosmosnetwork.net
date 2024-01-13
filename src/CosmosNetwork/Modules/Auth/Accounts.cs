namespace CosmosNetwork.Modules.Auth
{
    public record BaseAccount(CosmosAddress Address, int Sequence, string AccountNumber)
    {
        internal Serialization.Accounts.BaseAccount ToSerialization()
        {
            return new Serialization.Accounts.BaseAccount
            {
                Type = Serialization.Accounts.AccountTypes.BaseAccount,
                Address = Address,
                Sequence = Sequence,
                AccountNumber = AccountNumber,
            };
        }
    }

    public record DelayedVestingAccount(
        CosmosAddress Address,
        int Sequence,
        string AccountNumber,
        Coin[] OriginalVesting,
        Coin[] DelegatedFree,
        Coin[] DelegatedVesting,
        DateTime? StartTime,
        DateTime? EndTime) : BaseAccount(Address, Sequence, AccountNumber)
    {
        internal Serialization.Accounts.DelayedVestingAccount ToSerialization()
        {
            return new Serialization.Accounts.DelayedVestingAccount
            {
                Type = Serialization.Accounts.AccountTypes.DelayedVestingAccount,
                BaseVestingAccount = new Serialization.Accounts.BaseVestingAccount
                {
                    BaseAccount = new Serialization.Accounts.BaseAccount
                    {
                        Type = Serialization.Accounts.AccountTypes.BaseAccount,
                        Address = Address,
                        Sequence = Sequence,
                        AccountNumber = AccountNumber,
                    },
                    OriginalVesting = OriginalVesting.Select(c => c.ToSerialization()).ToArray(),
                    DelegatedFree = DelegatedFree.Select(c => c.ToSerialization()).ToArray(),
                    DelegatedVesting = DelegatedVesting.Select(c => c.ToSerialization()).ToArray(),
                    StartTime = StartTime,
                    EndTime = EndTime,
                },
            };
        }
    }

    public record PeriodicVestingAccount(
        CosmosAddress Address,
        int Sequence,
        string AccountNumber,
        Coin[] OriginalVesting,
        Coin[] DelegatedFree,
        Coin[] DelegatedVesting,
        DateTime StartTime,
        DateTime? EndTime,
        VestingPeriod[] VestingPeriods) : BaseAccount(Address, Sequence, AccountNumber)
    {
        internal Serialization.Accounts.PeriodicVestingAccount ToSerialization()
        {
            return new Serialization.Accounts.PeriodicVestingAccount
            {
                Type = Serialization.Accounts.AccountTypes.PeriodicVestingAccount,
                BaseVestingAccount = new Serialization.Accounts.BaseVestingAccount
                {
                    BaseAccount = new Serialization.Accounts.BaseAccount
                    {
                        Type = Serialization.Accounts.AccountTypes.BaseAccount,
                        Address = Address,
                        Sequence = Sequence,
                        AccountNumber = AccountNumber,
                    },
                    OriginalVesting = OriginalVesting.Select(c => c.ToSerialization()).ToArray(),
                    DelegatedFree = DelegatedFree.Select(c => c.ToSerialization()).ToArray(),
                    DelegatedVesting = DelegatedVesting.Select(c => c.ToSerialization()).ToArray(),
                    EndTime = EndTime,
                },
                StartTime = StartTime,
                VestingPeriods = VestingPeriods.Select(p => p.ToSerialization()).ToArray(),
            };
        }
    }

    public record ContinuousVestingAccount(
        CosmosAddress Address,
        int Sequence,
        string AccountNumber,
        Coin[] OriginalVesting,
        Coin[] DelegatedFree,
        Coin[] DelegatedVesting,
        DateTime StartTime,
        DateTime? EndTime) : BaseAccount(Address, Sequence, AccountNumber)
    {
        internal Serialization.Accounts.ContinuousVestingAccount ToSerialization()
        {
            return new Serialization.Accounts.ContinuousVestingAccount
            {
                Type = Serialization.Accounts.AccountTypes.PeriodicVestingAccount,
                BaseVestingAccount = new Serialization.Accounts.BaseVestingAccount
                {
                    BaseAccount = new Serialization.Accounts.BaseAccount
                    {
                        Type = Serialization.Accounts.AccountTypes.BaseAccount,
                        Address = Address,
                        Sequence = Sequence,
                        AccountNumber = AccountNumber,
                    },
                    OriginalVesting = OriginalVesting.Select(c => c.ToSerialization()).ToArray(),
                    DelegatedFree = DelegatedFree.Select(c => c.ToSerialization()).ToArray(),
                    DelegatedVesting = DelegatedVesting.Select(c => c.ToSerialization()).ToArray(),
                    EndTime = EndTime,
                },
                StartTime = StartTime,
            };
        }
    }

    public record ModuleAccount(
        CosmosAddress Address,
        int Sequence,
        string AccountNumber,
        string Name) : BaseAccount(Address, Sequence, AccountNumber)
    {
        internal Serialization.Accounts.ModuleAccount ToSerialization()
        {
            return new Serialization.Accounts.ModuleAccount
            {
                Type = Serialization.Accounts.AccountTypes.ModuleAccount,
                BaseAccount = new Serialization.Accounts.BaseAccount
                {
                    Type = Serialization.Accounts.AccountTypes.BaseAccount,
                    Address = Address,
                    Sequence = Sequence,
                    AccountNumber = AccountNumber,
                },
                Name = Name,
                Permissions = []
            };
        }
    }
}

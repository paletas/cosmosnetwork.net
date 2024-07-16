namespace CosmosNetwork.Modules.Auth
{
    public class AuthModule : ICosmosMessageModule
    {
        public void ConfigureModule(CosmosApiOptions cosmosOptions, CosmosMessageRegistry messageRegistry)
        {
            messageRegistry.RegisterMessage<Vesting.MessageCreatePermanentLockedAccount, Serialization.Vesting.MessageCreatePermanentLockedAccount>();
            messageRegistry.RegisterMessage<Vesting.MessageCreateVestingAccount, Serialization.Vesting.MessageCreateVestingAccount>();
            messageRegistry.RegisterMessage<Vesting.MessageCreatePeriodicVestingAccount, Serialization.Vesting.MessageCreatePeriodicVestingAccount>();
            messageRegistry.RegisterMessage<Vesting.MessageDonateAllVestingTokens, Serialization.Vesting.MessageDonateAllVestingTokens>();
        }
    }
}

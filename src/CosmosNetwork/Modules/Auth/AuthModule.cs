namespace CosmosNetwork.Modules.Auth
{
    public class AuthModule : ICosmosMessageModule
    {
        public void ConfigureModule(CosmosApiOptions cosmosOptions, CosmosMessageRegistry messageRegistry)
        {
            messageRegistry.RegisterMessage<Vesting.MessageCreatePermanentLockedAccount, Serialization.Vesting.MessageCreatePermanentLockedAccount>(
                Vesting.MessageCreatePermanentLockedAccount.COSMOS_DESCRIPTOR);
            messageRegistry.RegisterMessage<Vesting.MessageCreateVestingAccount, Serialization.Vesting.MessageCreateVestingAccount>(
                Vesting.MessageCreateVestingAccount.COSMOS_DESCRIPTOR);
            messageRegistry.RegisterMessage<Vesting.MessageCreatePeriodicVestingAccount, Serialization.Vesting.MessageCreatePeriodicVestingAccount>(
                Vesting.MessageCreatePeriodicVestingAccount.COSMOS_DESCRIPTOR);
            messageRegistry.RegisterMessage<Vesting.MessageDonateAllVestingTokens, Serialization.Vesting.MessageDonateAllVestingTokens>(
                Vesting.MessageDonateAllVestingTokens.COSMOS_DESCRIPTOR);
        }
    }
}

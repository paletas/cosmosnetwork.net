namespace Terra.NET
{
    public abstract record Message(MessageTypeEnum Type)
    {
        internal abstract API.Serialization.Json.Message ToJson();
    };

    public enum MessageTypeEnum
    {
        BankSend,
        BankMultiSend,

        MarketSwap,
        MarketSwapSend,

        OracleExchangeRateVote,
        OracleExchangeRatePrevote,
        OracleDelegateFeedConsent,

        StakingDelegate,
        StakingUndelegate,
        StakingCreateValidator,
        StakingBeginRedelegate,
        StakingSetWithdrawAddress,
        StakingEditValidator,

        DistributionFundCommunityPool,
        DistributionWithdrawRewards,
        DistributionWithdrawCommission,

        AuthzExecute,
        AuthzGrant,
        AuthzRevoke,

        FeeGrantAllowance,
        FeeGrantRevokeAllowance,

        WasmStoreCode,
        WasmMigrateCode,
        WasmInstantiateContract,
        WasmMigrateContract,
        WasmExecuteContract,
        WasmClearContractAdmin,
        WasmUpdateContractAdmin,

        GovDeposit,
        GovSubmitProposal,
        GovVote,
        GovWeightedVote,

        IbcUpdateClient,
        IbcReceivePacket,
        IbcTransfer,
        IbcAcknowledgement,
        IbcCreateClient,
        IbcChannelOpenInit,
        IbcChannelOpenTry,
        IbcChannelOpenConfirm,
        IbcChannelOpenAcknowledgement,
        IbcConnectionOpenInit,
        IbcConnectionOpenTry,
        IbcConnectionOpenConfirm,
        IbcConnectionOpenAcknowledgement,
        IbcTimeout
    }
}
using System.Numerics;
using Terra.NET.SmartContracts.Shared;

namespace Terra.NET.SmartContracts
{
    public record CW20Coin(string Address, ulong Amount);

    public record ExecuteCW20(
        CW20Burn? Burn = null,
        CW20Send? Send = null,
        CW20IncreaseAllowance? IncreaseAllowance = null,
        CW20DecreaseAllowance? DecreaseAllowance = null,
        CW20TransferFrom? TransferFrom = null,
        CW20SendFrom? SendFrom = null,
        CW20BurnFrom? BurnFrom = null,
        CW20Mint? Mint = null,
        CW20UpdateMarketing? UpdateMarketing = null
    );

    public record QueryCW20(
        CW20Balance? Balance = null,
        CW20TokenInfo? TokenInfo = null,
        CW20Minter? Minter = null,
        CW20Allowance? Allowance = null,
        CW20AllAllowances? AllAllowances = null,
        CW20AllAccounts? AllAccounts = null
    );

    public record CW20Burn(BigInteger Amount);

    public record CW20Send(string ContractAddress, BigInteger Amount, byte[] Message);

    public record CW20IncreaseAllowance(string Spender, BigInteger Amount, Expiration? Expiration);

    public record CW20DecreaseAllowance(string Spender, BigInteger Amount, Expiration? Expiration);

    public record CW20TransferFrom(string OwnerAddress, string RecipientAddress, BigInteger Amount);

    public record CW20SendFrom(string OwnerAddress, string ContractAddress, BigInteger Amount, byte[] Message);

    public record CW20BurnFrom(string OwnerAddress, BigInteger Amount);

    public record CW20Mint(string RecipientAddress, BigInteger Amount);

    public record CW20UpdateMarketing(string? ProjectUrl, string? Description, string? Marketing);

    public record CW20Balance(string Address);

    public record CW20BalanceResponse(BigInteger Balance);

    public record CW20TokenInfo();

    public record CW20TokenInfoResponse(string Name, string Symbol, ushort Decimals, BigInteger TotalSupply);

    public record CW20Minter();

    public record CW20MinterResponse(string Minter, BigInteger? Cap);

    public record CW20Allowance(string Owner, string Spender);

    public record CW20AllowanceResponse(BigInteger Allowance, Expiration Expires);

    public record CW20AllAllowances(string Owner, string? StartAfter, uint? Limit);

    public record CW20AllAllowancesResponse(CW20AllowanceInfo[] Allowances);

    public record CW20AllowanceInfo(string Spender, BigInteger Allowance, Expiration Expires);

    public record CW20AllAccounts(string? StartAfter, uint? Limit);

    public record CW20AllAccountsResponse(string[] Accounts);
}

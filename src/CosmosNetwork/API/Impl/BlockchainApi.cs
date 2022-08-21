using CosmosNetwork.Serialization.Json.Responses;
using Microsoft.Extensions.Logging;

namespace CosmosNetwork.API.Impl
{
    internal class BlockchainApi : BaseApiSection, IBlockchainApi
    {
        public BlockchainApi(CosmosApiOptions options, HttpClient httpClient, ILogger<BlockchainApi> logger) : base(options, httpClient, logger)
        {
        }

        public async Task<IReadOnlyDictionary<string, decimal>> GetGasPrices(CancellationToken cancellationToken = default)
        {
            return await Get<Dictionary<string, decimal>>("/v1/txs/gas_prices", cancellationToken).ConfigureAwait(false) ?? new Dictionary<string, decimal>();
        }

        public async Task<DenomMetadata[]> GetNativeDenoms(CancellationToken cancellationToken = default)
        {
            DenomMetadataResponse? metadataDenoms = await Get<DenomMetadataResponse>("/cosmos/bank/v1beta1/denoms_metadata", cancellationToken).ConfigureAwait(false);
            return metadataDenoms == null
                ? throw new InvalidOperationException()
                : metadataDenoms.Metadatas.Select(den =>
                new DenomMetadata(
                    den.Description,
                    den.Units.Select(denu => new DenomUnit(denu.Denom, denu.Decimals, denu.Aliases)).ToArray(),
                    den.BaseDenom,
                    den.DisplayDenom,
                    den.Name,
                    den.Symbol
                )
            ).ToArray();
        }

        public async Task<DenomSwapRate> GetSwapRate(string denomFrom, string denomTo, CancellationToken cancellationToken = default)
        {
            DenomSwapRate[]? swapRate = await Get<DenomSwapRate[]>($"/v1/market/swaprate/{denomFrom}", cancellationToken).ConfigureAwait(false);
            return swapRate == null ? throw new InvalidOperationException() : swapRate.Single(rate => rate.Denom == denomTo);
        }

        public async Task<DenomSwapRate[]> GetSwapRates(string denomFrom, CancellationToken cancellationToken = default)
        {
            DenomSwapRate[]? swapRates = await Get<DenomSwapRate[]>($"/terra/oracle/v1beta1/denoms/{denomFrom}/exchange_rate", cancellationToken).ConfigureAwait(false);
            return swapRates == null ? throw new InvalidOperationException() : swapRates;
        }

        public async Task<Coin> SimulateSwap(string denomFrom, ulong amountFrom, string denomTo, CancellationToken cancellationToken = default)
        {
            SimulateMarketSwapResponse? swapSimulation = await Get<SimulateMarketSwapResponse>($"/terra/market/v1beta1/swap?offer_coin={amountFrom}{denomFrom}&ask_denom={denomTo}", cancellationToken).ConfigureAwait(false);
            return swapSimulation == null
                ? throw new InvalidOperationException()
                : (Coin)new NativeCoin(swapSimulation.Result.Denom, swapSimulation.Result.Amount);
        }
    }
}

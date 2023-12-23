using CosmosNetwork.API;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Web;

namespace CosmosNetwork.Modules.Staking
{
    internal class StakingApi : CosmosApiModule, IStakingApi
    {
        public StakingApi(CosmosApiOptions options, IHttpClientFactory httpClientFactory, ILogger<StakingApi> logger) : base(options, httpClientFactory, logger)
        {
        }

        public StakingApi(
            [ServiceKey] string servicesKey,
            IServiceProvider serviceProvider,
            IHttpClientFactory httpClientFactory,
            ILogger<StakingApi> logger) : base(servicesKey, serviceProvider, httpClientFactory, logger)
        { }

        public async Task<StakingPool> GetStakingDetails(CancellationToken cancellationToken = default)
        {
            const string Url = "/cosmos/staking/v1beta1/pool";

            Serialization.Json.GetDelegationPoolResponse? response = await Get<Serialization.Json.GetDelegationPoolResponse>(Url, cancellationToken);

            return response is null ? throw new InvalidOperationException() : response.Pool.ToModel();
        }

        public async Task<DelegationParams> GetStakingParams(CancellationToken cancellationToken = default)
        {
            const string Url = "/cosmos/staking/v1beta1/params";

            Serialization.Json.GetDelegationParamsResponse? response = await Get<Serialization.Json.GetDelegationParamsResponse>(Url, cancellationToken);

            return response is null ? throw new InvalidOperationException() : response.Params.ToModel();
        }

        public async Task<Validator?> GetValidator(CosmosAddress validator, CancellationToken cancellationToken = default)
        {
            const string Url = "/cosmos/staking/v1beta1/validators/{0}";

            Serialization.Json.GetValidatorResponse? response = await Get<Serialization.Json.GetValidatorResponse>(PrepareEndpoint(Url, validator.Address), cancellationToken);

            return response?.Validator.ToModel();
        }

        public async Task<(DelegationBalance[] Delegations, Pagination Pagination)> GetValidatorDelegation(CosmosAddress validator, PaginationFilter? pagination = null, CancellationToken cancellationToken = default)
        {
            const string Url = "/cosmos/staking/v1beta1/validators/{0}/delegations";

            Serialization.Json.GetAddressDelegationsResponse? response = await Get<Serialization.Json.GetAddressDelegationsResponse>(PrepareEndpoint(Url, AsQueryParams(pagination), validator.Address), cancellationToken);

            return response is null
                ? ((DelegationBalance[] Delegations, Pagination Pagination))(Array.Empty<DelegationBalance>(), new Pagination(null, 0))
                : ((DelegationBalance[] Delegations, Pagination Pagination))(response.DelegationResponses.Select(d => d.ToModel()).ToArray(), response.Pagination.ToModel());
        }

        public async Task<DelegationBalance?> GetValidatorDelegatorBalance(CosmosAddress validator, CosmosAddress delegator, CancellationToken cancellationToken = default)
        {
            const string Url = "/cosmos/staking/v1beta1/validators/{0}/delegations/{1}";

            Serialization.Json.GetAddressDelegationResponse? response = await Get<Serialization.Json.GetAddressDelegationResponse>(PrepareEndpoint(Url, validator.Address, delegator.Address), cancellationToken);

            return response?.DelegationResponse.ToModel();
        }

        public async Task<(Validator[] Validators, Pagination Pagination)> GetValidators(PaginationFilter? pagination = null, CancellationToken cancellationToken = default)
        {
            const string Url = "/cosmos/staking/v1beta1/validators";

            Serialization.Json.GetValidatorsResponse? response = await Get<Serialization.Json.GetValidatorsResponse>(PrepareEndpoint(Url, AsQueryParams(pagination)), cancellationToken);

            return response is null
                ? ((Validator[] Validators, Pagination Pagination))(Array.Empty<Validator>(), new Pagination(null, 0))
                : ((Validator[] Validators, Pagination Pagination))(response.Validators.Select(d => d.ToModel()).ToArray(), response.Pagination.ToModel());
        }

        public async Task<(UnbondingDelegation[] UnbondingDelegations, Pagination Pagination)> GetValidatorUnbondingDelegations(CosmosAddress validator, PaginationFilter? pagination = null, CancellationToken cancellationToken = default)
        {
            const string Url = "/cosmos/staking/v1beta1/validators/{0}/unbonding_delegations";

            Serialization.Json.GetAddressUnbondingDelegationsResponse? response = await Get<Serialization.Json.GetAddressUnbondingDelegationsResponse>(PrepareEndpoint(Url, AsQueryParams(pagination), validator.Address), cancellationToken);

            return response is null
                ? ((UnbondingDelegation[] UnbondingDelegations, Pagination Pagination))(Array.Empty<UnbondingDelegation>(), new Pagination(null, 0))
                : ((UnbondingDelegation[] UnbondingDelegations, Pagination Pagination))(response.UnbondingResponses.Select(d => d.ToModel()).ToArray(), response.Pagination.ToModel());
        }

        public async Task<UnbondingDelegation?> GetValidatorUnbondingDelegator(CosmosAddress validator, CosmosAddress delegator, CancellationToken cancellationToken = default)
        {
            const string Url = "/cosmos/staking/v1beta1/validators/{0}/delegations/{1}/unbonding_delegations";

            Serialization.Json.GetAddressUnbondingDelegationResponse? response = await Get<Serialization.Json.GetAddressUnbondingDelegationResponse>(PrepareEndpoint(Url, validator.Address, delegator.Address), cancellationToken);

            return response?.UnbondingResponse.ToModel();
        }

        public async Task<(DelegationBalance[] Delegations, Pagination Pagination)> GetWalletDelegation(CosmosAddress delegator, PaginationFilter? pagination = null, CancellationToken cancellationToken = default)
        {
            const string Url = "/cosmos/staking/v1beta1/delegations/{0}";

            Serialization.Json.GetAddressDelegationsResponse? response = await Get<Serialization.Json.GetAddressDelegationsResponse>(PrepareEndpoint(Url, AsQueryParams(pagination), delegator.Address), cancellationToken);

            return response is null
                ? ((DelegationBalance[] Delegations, Pagination Pagination))(Array.Empty<DelegationBalance>(), new Pagination(null, 0))
                : ((DelegationBalance[] Delegations, Pagination Pagination))(response.DelegationResponses.Select(d => d.ToModel()).ToArray(), response.Pagination.ToModel());
        }

        public async Task<(Redelegation[] Redelegations, Pagination Pagination)> GetWalletRedelegations(CosmosAddress delegator, CosmosAddress? fromValidator = null, CosmosAddress? toValidator = null, PaginationFilter? pagination = null, CancellationToken cancellationToken = default)
        {
            const string Url = "/cosmos/staking/v1beta1/delegations/{0}/redelegations";

            Serialization.Json.GetAddressRedelegationsResponse? response = await Get<Serialization.Json.GetAddressRedelegationsResponse>(PrepareEndpoint(Url, AsRedelegationsQueryParams(fromValidator, toValidator, pagination), delegator.Address), cancellationToken);

            return response is null
                ? ((Redelegation[] Redelegations, Pagination Pagination))(Array.Empty<Redelegation>(), new Pagination(null, 0))
                : ((Redelegation[] Redelegations, Pagination Pagination))(response.RedelegationResponses.Select(d => d.ToModel()).ToArray(), response.Pagination.ToModel());
        }

        public async Task<(UnbondingDelegation[] UnbondingDelegations, Pagination Pagination)> GetWalletUnbondingDelegations(CosmosAddress delegator, PaginationFilter? pagination = null, CancellationToken cancellationToken = default)
        {
            const string Url = "/cosmos/staking/v1beta1/delegations/{0}/unbonding_delegations";

            Serialization.Json.GetAddressUnbondingDelegationsResponse? response = await Get<Serialization.Json.GetAddressUnbondingDelegationsResponse>(PrepareEndpoint(Url, AsQueryParams(pagination), delegator.Address), cancellationToken);

            return response is null
                ? ((UnbondingDelegation[] UnbondingDelegations, Pagination Pagination))(Array.Empty<UnbondingDelegation>(), new Pagination(null, 0))
                : ((UnbondingDelegation[] UnbondingDelegations, Pagination Pagination))(response.UnbondingResponses.Select(d => d.ToModel()).ToArray(), response.Pagination.ToModel());
        }

        public async Task<Validator?> GetWalletValidator(CosmosAddress delegator, CosmosAddress validator, CancellationToken cancellationToken = default)
        {
            const string Url = "/cosmos/staking/v1beta1/delegators/{0}/validators/{1}";

            Serialization.Json.GetValidatorResponse? response = await Get<Serialization.Json.GetValidatorResponse>(PrepareEndpoint(Url, delegator.Address, validator.Address), cancellationToken);

            return response?.Validator.ToModel();
        }

        public async Task<(Validator[] Validators, Pagination Pagination)> GetWalletValidators(CosmosAddress delegator, PaginationFilter? pagination = null, CancellationToken cancellationToken = default)
        {
            const string Url = "/cosmos/staking/v1beta1/delegators/{0}/validators";

            Serialization.Json.GetValidatorsResponse? response = await Get<Serialization.Json.GetValidatorsResponse>(PrepareEndpoint(Url, AsQueryParams(pagination), delegator.Address), cancellationToken);

            return response is null
                ? ((Validator[] Validators, Pagination Pagination))(Array.Empty<Validator>(), new Pagination(null, 0))
                : ((Validator[] Validators, Pagination Pagination))(response.Validators.Select(d => d.ToModel()).ToArray(), response.Pagination.ToModel());
        }

        private IDictionary<string, string> AsQueryParams(PaginationFilter? paginationFilter)
        {
            Dictionary<string, string> queryParams = new();

            if (paginationFilter is null)
            {
                return queryParams;
            }

            if (paginationFilter.Limit.HasValue)
            {
                queryParams.Add("pagination.limit", paginationFilter.Limit.Value.ToString());
            }

            if (paginationFilter.Continuation is not null)
            {
                if (paginationFilter.Continuation.Key is not null)
                {
                    queryParams.Add("pagination.key", paginationFilter.Continuation.Key);
                }
                else if (paginationFilter.Continuation.Offset.HasValue)
                {
                    queryParams.Add("pagination.key", paginationFilter.Continuation.Offset.Value.ToString());
                }
            }

            queryParams.Add("pagination.count_total", paginationFilter.CountTotal.ToString());

            return queryParams;
        }


        private IDictionary<string, string> AsRedelegationsQueryParams(CosmosAddress? fromAddress, CosmosAddress? toAddress, PaginationFilter? paginationFilter)
        {
            IDictionary<string, string> queryParams = AsQueryParams(paginationFilter);

            if (fromAddress is not null)
            {
                queryParams.Add("src_validator_addr", fromAddress);
            }

            if (toAddress is not null)
            {
                queryParams.Add("dst_validator_addr", toAddress);
            }

            return queryParams;
        }

        private Uri PrepareEndpoint(string url, params object[] args)
        {
            return new Uri(string.Format(url, args), UriKind.Relative);
        }

        private Uri PrepareEndpoint(string url, IDictionary<string, string> queryParams, params object[] args)
        {
            System.Collections.Specialized.NameValueCollection queryString = HttpUtility.ParseQueryString(string.Empty);
            foreach ((string name, string value) in queryParams)
            {
                queryString.Add(name, value);
            }

            return new Uri(string.Format($"{url}?{queryString}", args), UriKind.Relative);
        }
    }
}

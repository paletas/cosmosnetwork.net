using CosmosNetwork.API;
using Microsoft.Extensions.Logging;
using System.Web;

namespace CosmosNetwork.Modules.Staking
{
    internal class StakingApi : BaseApiSection, IStakingApi
    {
        public StakingApi(CosmosApiOptions options, HttpClient httpClient, ILogger<BaseApiSection> logger) : base(options, httpClient, logger)
        {
        }

        public async Task<StakingPool> GetStakingDetails(CancellationToken cancellationToken = default)
        {
            const string Url = "/cosmos/staking/v1beta1/pool";

            var response = await this.Get<Serialization.Json.GetDelegationPoolResponse>(Url, cancellationToken);

            if (response is null) throw new InvalidOperationException();

            return response.Pool.ToModel();
        }

        public async Task<DelegationParams> GetStakingParams(CancellationToken cancellationToken = default)
        {
            const string Url = "/cosmos/staking/v1beta1/params";

            var response = await this.Get<Serialization.Json.GetDelegationParamsResponse>(Url, cancellationToken);

            if (response is null) throw new InvalidOperationException();

            return response.Params.ToModel();
        }

        public async Task<Validator?> GetValidator(CosmosAddress validator, CancellationToken cancellationToken = default)
        {
            const string Url = "/cosmos/staking/v1beta1/validators/{0}";

            var response = await this.Get<Serialization.Json.GetValidatorResponse>(PrepareEndpoint(Url, validator.Address), cancellationToken);

            return response?.Validator.ToModel();
        }

        public async Task<(DelegationBalance[] Delegations, Pagination Pagination)> GetValidatorDelegation(CosmosAddress validator, PaginationFilter? pagination = null, CancellationToken cancellationToken = default)
        {
            const string Url = "/cosmos/staking/v1beta1/validators/{0}/delegations";

            var response = await this.Get<Serialization.Json.GetAddressDelegationsResponse>(PrepareEndpoint(Url, AsQueryParams(pagination), validator.Address), cancellationToken);

            if (response is null)
                return (Array.Empty<DelegationBalance>(), new Pagination(null, 0));
            else
                return (response.DelegationResponses.Select(d => d.ToModel()).ToArray(), response.Pagination.ToModel());
        }

        public async Task<DelegationBalance?> GetValidatorDelegatorBalance(CosmosAddress validator, CosmosAddress delegator, CancellationToken cancellationToken = default)
        {
            const string Url = "/cosmos/staking/v1beta1/validators/{0}/delegations/{1}";

            var response = await this.Get<Serialization.Json.GetAddressDelegationResponse>(PrepareEndpoint(Url, validator.Address, delegator.Address), cancellationToken);

            return response?.DelegationResponse.ToModel();
        }

        public async Task<(Validator[] Validators, Pagination Pagination)> GetValidators(PaginationFilter? pagination = null, CancellationToken cancellationToken = default)
        {
            const string Url = "/cosmos/staking/v1beta1/validators";

            var response = await this.Get<Serialization.Json.GetValidatorsResponse>(PrepareEndpoint(Url, AsQueryParams(pagination)), cancellationToken);

            if (response is null)
                return (Array.Empty<Validator>(), new Pagination(null, 0));
            else
                return (response.Validators.Select(d => d.ToModel()).ToArray(), response.Pagination.ToModel());
        }

        public async Task<(UnbondingDelegation[] UnbondingDelegations, Pagination Pagination)> GetValidatorUnbondingDelegations(CosmosAddress validator, PaginationFilter? pagination = null, CancellationToken cancellationToken = default)
        {
            const string Url = "/cosmos/staking/v1beta1/validators/{0}/unbonding_delegations";

            var response = await this.Get<Serialization.Json.GetAddressUnbondingDelegationsResponse>(PrepareEndpoint(Url, AsQueryParams(pagination), validator.Address), cancellationToken);

            if (response is null)
                return (Array.Empty<UnbondingDelegation>(), new Pagination(null, 0));
            else
                return (response.UnbondingResponses.Select(d => d.ToModel()).ToArray(), response.Pagination.ToModel());
        }

        public async Task<UnbondingDelegation?> GetValidatorUnbondingDelegator(CosmosAddress validator, CosmosAddress delegator, CancellationToken cancellationToken = default)
        {
            const string Url = "/cosmos/staking/v1beta1/validators/{0}/delegations/{1}/unbonding_delegations";

            var response = await this.Get<Serialization.Json.GetAddressUnbondingDelegationResponse>(PrepareEndpoint(Url, validator.Address, delegator.Address), cancellationToken);

            return response?.UnbondingResponse.ToModel();
        }

        public async Task<(DelegationBalance[] Delegations, Pagination Pagination)> GetWalletDelegation(CosmosAddress delegator, PaginationFilter? pagination = null, CancellationToken cancellationToken = default)
        {
            const string Url = "/cosmos/staking/v1beta1/delegations/{0}";

            var response = await this.Get<Serialization.Json.GetAddressDelegationsResponse>(PrepareEndpoint(Url, AsQueryParams(pagination), delegator.Address), cancellationToken);

            if (response is null)
                return (Array.Empty<DelegationBalance>(), new Pagination(null, 0));
            else
                return (response.DelegationResponses.Select(d => d.ToModel()).ToArray(), response.Pagination.ToModel());
        }

        public async Task<(Redelegation[] Redelegations, Pagination Pagination)> GetWalletRedelegations(CosmosAddress delegator, CosmosAddress? fromValidator = null, CosmosAddress? toValidator = null, PaginationFilter? pagination = null, CancellationToken cancellationToken = default)
        {
            const string Url = "/cosmos/staking/v1beta1/delegations/{0}/redelegations";

            var response = await this.Get<Serialization.Json.GetAddressRedelegationsResponse>(PrepareEndpoint(Url, AsRedelegationsQueryParams(fromValidator, toValidator, pagination), delegator.Address), cancellationToken);

            if (response is null)
                return (Array.Empty<Redelegation>(), new Pagination(null, 0));
            else
                return (response.RedelegationResponses.Select(d => d.ToModel()).ToArray(), response.Pagination.ToModel());
        }

        public async Task<(UnbondingDelegation[] UnbondingDelegations, Pagination Pagination)> GetWalletUnbondingDelegations(CosmosAddress delegator, PaginationFilter? pagination = null, CancellationToken cancellationToken = default)
        {
            const string Url = "/cosmos/staking/v1beta1/delegations/{0}/unbonding_delegations";

            var response = await this.Get<Serialization.Json.GetAddressUnbondingDelegationsResponse>(PrepareEndpoint(Url, AsQueryParams(pagination), delegator.Address), cancellationToken);

            if (response is null)
                return (Array.Empty<UnbondingDelegation>(), new Pagination(null, 0));
            else
                return (response.UnbondingResponses.Select(d => d.ToModel()).ToArray(), response.Pagination.ToModel());
        }

        public async Task<Validator?> GetWalletValidator(CosmosAddress delegator, CosmosAddress validator, CancellationToken cancellationToken = default)
        {
            const string Url = "/cosmos/staking/v1beta1/delegators/{0}/validators/{1}";

            var response = await this.Get<Serialization.Json.GetValidatorResponse>(PrepareEndpoint(Url, delegator.Address, validator.Address), cancellationToken);

            return response?.Validator.ToModel();
        }

        public async Task<(Validator[] Validators, Pagination Pagination)> GetWalletValidators(CosmosAddress delegator, PaginationFilter? pagination = null, CancellationToken cancellationToken = default)
        {
            const string Url = "/cosmos/staking/v1beta1/delegators/{0}/validators";

            var response = await this.Get<Serialization.Json.GetValidatorsResponse>(PrepareEndpoint(Url, AsQueryParams(pagination), delegator.Address), cancellationToken);

            if (response is null)
                return (Array.Empty<Validator>(), new Pagination(null, 0));
            else
                return (response.Validators.Select(d => d.ToModel()).ToArray(), response.Pagination.ToModel());
        }

        private IDictionary<string, string> AsQueryParams(PaginationFilter? paginationFilter)
        {
            var queryParams = new Dictionary<string, string>();

            if (paginationFilter is null) return queryParams;

            if (paginationFilter.Limit.HasValue)
                queryParams.Add("pagination.limit", paginationFilter.Limit.Value.ToString());

            if (paginationFilter.Continuation is not null)
            {
                if (paginationFilter.Continuation.Key is not null)
                    queryParams.Add("pagination.key", paginationFilter.Continuation.Key);
                else if (paginationFilter.Continuation.Offset.HasValue)
                    queryParams.Add("pagination.key", paginationFilter.Continuation.Offset.Value.ToString());
            }

            queryParams.Add("pagination.count_total", paginationFilter.CountTotal.ToString());

            return queryParams;
        }


        private IDictionary<string, string> AsRedelegationsQueryParams(CosmosAddress? fromAddress, CosmosAddress? toAddress, PaginationFilter? paginationFilter)
        {
            var queryParams = AsQueryParams(paginationFilter);

            if (fromAddress is not null)
                queryParams.Add("src_validator_addr", fromAddress);

            if (toAddress is not null)
                queryParams.Add("dst_validator_addr", toAddress);

            return queryParams;
        }

        private Uri PrepareEndpoint(string url, params object[] args)
        {
            return new Uri(string.Format(url, args), UriKind.Relative);
        }

        private Uri PrepareEndpoint(string url, IDictionary<string, string> queryParams, params object[] args)
        {
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            foreach (var (name, value) in queryParams)
            {
                queryString.Add(name, value);
            }

            return new Uri(string.Format($"{url}?{queryString}", args), UriKind.Relative);
        }
    }
}

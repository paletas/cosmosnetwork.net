﻿using CosmosNetwork.API;
using Microsoft.Extensions.Logging;

namespace CosmosNetwork.Modules.Gov
{
    internal class GovApi : CosmosApiModule, IGovApi
    {
        public GovApi(CosmosApiOptions options, IHttpClientFactory httpClientFactory, ILogger<CosmosApiModule> logger) : base(options, httpClientFactory, logger)
        { }

        public async Task<GovParams> GetGovernanceParams(CancellationToken cancellationToken = default)
        {
            var depositParams = await GetGovernanceDepositParams(cancellationToken);
            var votingParams = await GetGovernanceVotingParams(cancellationToken);
            var tallyingParams = await GetGovernanceTallyParams(cancellationToken);

            return new GovParams(votingParams, depositParams, tallyingParams);
        }

        public async Task<GovDepositParams> GetGovernanceDepositParams(CancellationToken cancellationToken = default)
        {
            const string Url = "/cosmos/gov/v1beta1/params/{0}";

            Serialization.Json.GovParams? response = await Get<Serialization.Json.GovParams>(string.Format(Url, "deposit"), cancellationToken);
            if (response is null) throw new InvalidOperationException();
            return response.DepositParams.ToModel();
        }

        public async Task<GovTallyParams> GetGovernanceTallyParams(CancellationToken cancellationToken = default)
        {
            const string Url = "/cosmos/gov/v1beta1/params/{0}";

            Serialization.Json.GovParams? response = await Get<Serialization.Json.GovParams>(string.Format(Url, "tallying"), cancellationToken);
            if (response is null) throw new InvalidOperationException();
            return response.TallyParams.ToModel();
        }

        public async Task<GovVotingParams> GetGovernanceVotingParams(CancellationToken cancellationToken = default)
        {
            const string Url = "/cosmos/gov/v1beta1/params/{0}";

            Serialization.Json.GovParams? response = await Get<Serialization.Json.GovParams>(string.Format(Url, "voting"), cancellationToken);
            if (response is null) throw new InvalidOperationException();
            return response.VotingParams.ToModel();
        }

        public async Task<Proposal?> GetProposal(ulong id, CancellationToken cancellationToken = default)
        {
            const string Url = "/cosmos/gov/v1beta1/proposals/{0}";

            Serialization.Json.GetProposalResponse? response = await Get<Serialization.Json.GetProposalResponse>(string.Format(Url, id), cancellationToken);
            return response?.Proposal.ToModel();
        }
    }
}

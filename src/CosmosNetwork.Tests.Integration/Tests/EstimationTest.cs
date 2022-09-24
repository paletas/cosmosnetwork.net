using CosmosNetwork.Modules.Staking;
using CosmosNetwork.Modules.Staking.Messages;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace CosmosNetwork.Tests.Integration.Tests
{
    internal class EstimationTest : IIntegrationTest
    {
        private static Random _Randomizer = new Random();

        private readonly ILogger<EstimationTest> _logger;
        private readonly CosmosApi _cosmosApi;

        public EstimationTest(ILogger<EstimationTest> logger, CosmosApi cosmosApi)
        {
            this._logger = logger;
            this._cosmosApi = cosmosApi;
        }

        public string Name => "Estimate Endpoint";

        public async Task Execute(IWallet wallet, CancellationToken cancellationToken = default)
        {
            if (wallet.AccountAddress is null)
                throw new InvalidOperationException();

            var stakingSettings = await _cosmosApi.Staking.GetStakingParams(cancellationToken);
            var validatorsAvailable = await _cosmosApi.Staking.GetValidators(cancellationToken: cancellationToken);

            var randomValidator = validatorsAvailable.Validators.ElementAt(_Randomizer.Next(validatorsAvailable.Validators.Length));

            this._logger.LogInformation("Picked validator {validatorAddress}", randomValidator.Operator);

            this._logger.LogInformation("Estimate Tx 1");

            IEnumerable<Message> messages = new Message[]
            {
                new MessageDelegate(wallet.AccountAddress, randomValidator.Operator, new NativeCoin(stakingSettings.BondDenom, 10000000))
            };

            var signedTx = await wallet.CreateSignedTransaction(messages, new CreateTransactionOptions(), cancellationToken);
            var simulationResults = await this._cosmosApi.Transactions.SimulateTransaction(signedTx, cancellationToken);
        }
    }
}

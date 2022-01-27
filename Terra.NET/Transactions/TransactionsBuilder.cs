using Cosmos.SDK.Protos.Tx;
using Cosmos.SDK.Protos.Tx.Signing;
using Microsoft.Extensions.Logging;
using Terra.NET.API.Serialization.Protos.Mappers;

namespace Terra.NET.Transactions
{
    internal class TransactionsBuilder
    {
        private readonly TerraApiOptions _options;
        private readonly ILogger<TransactionsBuilder> _logger;

        public TransactionsBuilder(TerraApiOptions options, ILogger<TransactionsBuilder> logger)
        {
            this._options = options;
            this._logger = logger;
        }

        public Tx CreateTransaction(IEnumerable<Message> messages, string? memo = null, ulong? timeoutHeight = null)
        {
            var transaction = new Tx
            {
                Body = new TxBody()
                {
                    Memo = memo ?? string.Empty,
                    TimeoutHeight = timeoutHeight ?? default
                },
                AuthInfo = new AuthInfo
                {
                    Fee = new Cosmos.SDK.Protos.Tx.Fee()
                }
            };

            transaction.Body.Messages.AddRange(messages.Select(msg => msg.ToJson().PackAny(this._options.JsonSerializerOptions)));

            return transaction;
        }

        public Tx AddEmptySignatures(Tx transaction, SignerOptions[] signers)
        {
            foreach (var signer in signers)
            {
                var signerInfo = new SignerInfo
                {
                    PublicKey = signer.PublicKey?.PackAny(),
                    Sequence = signer.Sequence,
                    ModeInfo = new ModeInfo
                    {
                        Single = new ModeInfo.Types.Single
                        {
                            Mode = SignMode.Direct
                        }
                    }
                };

                transaction.Signatures.Add(Google.Protobuf.ByteString.Empty);
                transaction.AuthInfo.SignerInfos.Add(signerInfo);
            }

            return transaction;
        }
    }
}

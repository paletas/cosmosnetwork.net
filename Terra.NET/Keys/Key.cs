using Cosmos.SDK.Protos.Tx;
using Cosmos.SDK.Protos.Tx.Signing;
using Google.Protobuf;

namespace Terra.NET.Keys
{
    internal abstract class Key : IKey
    {
        protected byte[] PrivateKey { get; private set; } = null!;

        public PublicKey PublicKey { get; private set; } = null!;

        protected void SetKeys(byte[] privateKey, byte[] publicKey)
        {
            this.PrivateKey = privateKey;
            this.PublicKey = new PublicKey(publicKey);
        }

        private TransactionSigner CreateSignature(string chain, SignerOptions signer, SignerInfo signerInfo, SignerModeEnum signerMode, AuthInfo authInfo, TxBody txBody)
        {
            var authInfoCopy = new AuthInfo();
            authInfoCopy.Fee = authInfo.Fee;
            authInfoCopy.SignerInfos.Add(signerInfo);

            var signDoc = new SignDoc
            {
                ChainId = chain,
                AccountNumber = ulong.Parse(signer.AccountNumber),
                AuthInfoBytes = authInfoCopy.ToByteString(),
                BodyBytes = txBody.ToByteString()
            };

            byte[] signBytes = SignPayload(signDoc.ToByteArray());
            string signBase64 = Convert.ToBase64String(signBytes);

            return new TransactionSigner(this.PublicKey, new SignatureDescriptor(signerMode, signBase64), signer.Sequence);
        }

        public abstract byte[] SignPayload(byte[] payload);

        public Tx SignTransaction(Tx transaction, SignerOptions[] signers, SignOptions signOptions)
        {
            var txClone = new Tx(transaction);

            var signerMode = SignerModeEnum.Direct;
            foreach (var signer in signers)
            {
                var signerInfo = new SignerInfo
                {
                    PublicKey = signer.PublicKey.ToJson().PackAny(),
                    ModeInfo = new ModeInfo { Single = new ModeInfo.Types.Single { Mode = ToJson(signerMode) } },
                    Sequence = signer.Sequence
                };

                var signature = CreateSignature(signOptions.ChainId, signer, signerInfo, signerMode, txClone.AuthInfo, txClone.Body);

                txClone.AuthInfo.SignerInfos.Add(signerInfo);
                txClone.Signatures.Add(ByteString.FromBase64(signature.Data.Signature));
            }

            return txClone;
        }

        private static SignMode ToJson(SignerModeEnum signerMode)
        {
            return signerMode switch
            {
                SignerModeEnum.AminoLegacy => SignMode.LegacyAminoJson,
                SignerModeEnum.Textual => SignMode.Textual,
                SignerModeEnum.Direct => SignMode.Direct,
                _ => throw new ArgumentException(null, nameof(signerMode)),
            };
        }
    }
}

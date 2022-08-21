using CosmosNetwork.Serialization;

namespace CosmosNetwork.Keys
{
    internal abstract class Key : IKey
    {
        protected byte[] PrivateKey { get; private set; } = null!;

        public PublicKey PublicKey { get; private set; } = null!;

        protected void SetKeys(byte[] privateKey, byte[] publicKey)
        {
            PrivateKey = privateKey;
            PublicKey = new PublicKey(publicKey);
        }

        private TransactionSigner CreateSignature(string chain, SignerOptions signer, Serialization.SignatureDescriptor signerInfo, SignerModeEnum signerMode, AuthInfo authInfo, TransactionBody txBody)
        {
            AuthInfo authInfoCopy = new(new List<Serialization.SignatureDescriptor> { signerInfo }, authInfo.Fee);
            SignDoc signDoc = new(txBody, authInfoCopy, chain, ulong.Parse(signer.AccountNumber));

            byte[] signBytes = SignPayload(signDoc.ToByteArray());
            string signBase64 = Convert.ToBase64String(signBytes);

            return new TransactionSigner(PublicKey.ToSignatureKey(), new SignatureDescriptor(signerMode, signBase64), signer.Sequence);
        }

        public abstract byte[] SignPayload(byte[] payload);

        public Serialization.Transaction SignTransaction(Transaction transaction, SignerOptions[] signers, SignOptions signOptions)
        {
            throw new NotImplementedException();
        }
    }
}

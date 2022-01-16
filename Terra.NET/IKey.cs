using Cosmos.SDK.Protos.Tx;

namespace Terra.NET
{
    internal interface IKey
    {
        PublicKey PublicKey { get; }

        Tx SignTransaction(Tx transaction, SignerOptions[] signers, SignOptions signOptions);
    }
}

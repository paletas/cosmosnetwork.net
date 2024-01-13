namespace CosmosNetwork.Modules.Auth
{
    public record AuthParams(
        int MaxMemoCharacters, 
        int SignatureVerifyCostForEd25519, 
        int SignatureVerifyCostForSecp256k1, 
        int TransactionSignaturesLimit,
        int TransactionSizeCostPerByte)
    {
        internal Serialization.AuthParams ToSerialization()
        {
            return new Serialization.AuthParams
            {
                MaxMemoCharacters = this.MaxMemoCharacters,
                SigVerifyCostEd25519 = this.SignatureVerifyCostForEd25519,
                SigVerifyCostSecp256k1 = this.SignatureVerifyCostForSecp256k1,
                TxSigLimit = this.TransactionSignaturesLimit,
                TxSizeCostPerByte = this.TransactionSizeCostPerByte,
            };
        }
    }
}

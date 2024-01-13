namespace CosmosNetwork.Modules.Auth.Serialization
{
    public class AuthParams
    {
        public int MaxMemoCharacters { get; set; }

        public int SigVerifyCostEd25519 { get; set; }

        public int SigVerifyCostSecp256k1 { get; set; }

        public int TxSigLimit { get; set; }

        public int TxSizeCostPerByte { get; set; }

        public Auth.AuthParams ToModel()
        {
            return new Auth.AuthParams(
                this.MaxMemoCharacters,
                this.SigVerifyCostEd25519,
                this.SigVerifyCostSecp256k1,
                this.TxSigLimit,
                this.TxSizeCostPerByte);
        }
    }
}

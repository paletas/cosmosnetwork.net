namespace CosmosNetwork.Keys
{
    internal class MnemonicKey : Key
    {
        public MnemonicKey(string mnemonic, MnemonicKeyOptions? options = null)
        {
            options ??= new MnemonicKeyOptions();

            var wallet = new Nethereum.HdWallet.Wallet(mnemonic, string.Empty, GetLunaHdPath(options.CoinType, options.Account, options.Index));
            byte[] privateKey = wallet.GetPrivateKey((int)options.Index);
            byte[] publicKey = Cryptography.ECDSA.Secp256K1Manager.GetPublicKey(privateKey, true);

            SetKeys(privateKey, publicKey);
        }

        public override byte[] SignPayload(byte[] payload)
        {
            int recoveryId;
            var hash = HashExtensions.SHA256(payload);
            return Cryptography.ECDSA.Secp256K1Manager.SignCompact(hash, PrivateKey, out recoveryId);
        }

        private static string GetLunaHdPath(string coinType, uint account, uint index)
        {
            return $"m/44'/{coinType}'/{account}'/0/x";
        }
    }

    internal class MnemonicKeyOptions
    {
        public const string LUNA_COINTYPE = "330";

        public string CoinType { get; set; } = LUNA_COINTYPE;

        public uint Account { get; set; } = 0;

        public uint Index { get; set; } = 0;
    }
}

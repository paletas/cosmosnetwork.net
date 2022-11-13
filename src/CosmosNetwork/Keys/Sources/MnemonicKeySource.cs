namespace CosmosNetwork.Keys.Sources
{
    internal class MnemonicKeySource : BasicKeySource
    {
        public MnemonicKeySource(string mnemonic, MnemonicKeyOptions options, NetworkOptions network)
        {
            Nethereum.HdWallet.Wallet wallet = new(mnemonic, string.Empty, GetHdPath(network.CoinType, options.Account, options.Index));
            byte[] privateKey = wallet.GetPrivateKey((int)options.Index);
            byte[] publicKey = Cryptography.ECDSA.Secp256K1Manager.GetPublicKey(privateKey, true);

            base.SetKeys(new BasicPrivateKey(privateKey), new BasicPublicKey(publicKey, BasicPublicKey.CurveAlgorithm.Secp256k1));
        }
        
        private static string GetHdPath(string coinType, uint account, uint index)
        {
            return $"m/44'/{coinType}'/{account}'/0/x";
        }
    }

    public class MnemonicKeyOptions
    {
        public MnemonicKeyOptions(uint? account = null, uint? index = null)
        {
            this.Account = account ?? 0;
            this.Index = index ?? 0;
        }

        public uint Account { get; set; }

        public uint Index { get; set; }
    }
}

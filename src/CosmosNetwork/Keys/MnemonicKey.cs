﻿using CosmosNetwork.Serialization;

namespace CosmosNetwork.Keys
{
    internal class MnemonicKey : Key
    {
        public MnemonicKey(string mnemonic, MnemonicKeyOptions options)
        {
            Nethereum.HdWallet.Wallet wallet = new(mnemonic, string.Empty, GetHdPath(options.CoinType, options.Account, options.Index));
            byte[] privateKey = wallet.GetPrivateKey((int)options.Index);
            byte[] publicKey = Cryptography.ECDSA.Secp256K1Manager.GetPublicKey(privateKey, true);

            base.SetKeys(privateKey, publicKey);
        }

        public override Task<byte[]> SignPayload(byte[] payload, CancellationToken cancellationToken = default)
        {
            byte[] hash = HashExtensions.SHA256(payload);
            return Task.FromResult(Cryptography.ECDSA.Secp256K1Manager.SignCompact(hash, PrivateKey, out _));
        }

        public override Task<byte[]> SignTransaction(string chainId, ulong accountNumber, Transaction transaction, CancellationToken cancellationToken = default)
        {
            return SignPayload(transaction.ToSignatureDocument(chainId, accountNumber).ToByteArray(), cancellationToken);
        }

        private static string GetHdPath(string coinType, uint account, uint index)
        {
            return $"m/44'/{coinType}'/{account}'/0/x";
        }
    }

    public class MnemonicKeyOptions
    {
        public MnemonicKeyOptions(string coinType, uint? account = null, uint? index = null)
        {
            CoinType = coinType;
            Account = account ?? 0;
            Index = index ?? 0;
        }

        public string CoinType { get; set; }

        public uint Account { get; set; }

        public uint Index { get; set; }
    }
}

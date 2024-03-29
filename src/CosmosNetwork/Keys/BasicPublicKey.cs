﻿using DevHawk.Security.Cryptography;
using System.Security.Cryptography;

namespace CosmosNetwork.Keys
{
    internal class BasicPublicKey : IPublicKey
    {
        private readonly CurveAlgorithm _curveAlgorithm;

        public enum CurveAlgorithm
        {
            Secp256k1 = 1,
            Ed25519 = 2
        }

        public BasicPublicKey(byte[] key, CurveAlgorithm curveAlgorithm)
        {
            this.RawKey = key;
            this._curveAlgorithm = curveAlgorithm;
        }

        public BasicPublicKey(string key, CurveAlgorithm curveAlgorithm)
            : this(Convert.FromBase64String(key), curveAlgorithm)
        { }

        public byte[] RawKey { get; }

        public byte[] RawAddress => new RIPEMD160().ComputeHash(SHA256.HashData(this.RawKey));

        public string AsAddress(string prefix) => Bech32.Encode(prefix, this.RawAddress);

        public Serialization.Json.PublicKey ToJson()
        {
            return this._curveAlgorithm switch
            {
                CurveAlgorithm.Secp256k1 => new Serialization.Json.Secp256k1(Convert.ToBase64String(this.RawKey)),
                CurveAlgorithm.Ed25519 => new Serialization.Json.Ed25519(Convert.ToBase64String(this.RawKey)),
                _ => throw new NotImplementedException()
            };
        }

        public Serialization.Proto.PublicKey ToProto()
        {
            return new Serialization.Proto.Secp256k1(Convert.ToBase64String(this.RawKey));
        }
    }
}

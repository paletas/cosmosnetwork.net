using CosmosNetwork.Keys;
using System.Text.Json.Serialization;

namespace CosmosNetwork.Genesis.Serialization
{
    internal enum PublicKeyTypes
    {
        Ed25519 = KeyCurveAlgorithm.Ed25519,
        Secp256k1 = KeyCurveAlgorithm.Secp256k1,
    }
}

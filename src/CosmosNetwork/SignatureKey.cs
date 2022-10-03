namespace CosmosNetwork
{
    public abstract record SignatureKey(SignatureTypeEnum Type)
    {
        public abstract Serialization.Proto.SimplePublicKey ToProto();

        public abstract Serialization.Json.PublicKey ToJson();
    }

    public enum SignatureTypeEnum
    {
        Secp256k1,
        Ed25519,
        Multisig
    }

    public record Secp256k1Key(string Key) : SignatureKey(SignatureTypeEnum.Secp256k1)
    {
        public override Serialization.Json.PublicKey ToJson()
        {
            return new Serialization.Json.Secp256k1(Key);
        }

        public override Serialization.Proto.SimplePublicKey ToProto()
        {
            return new Serialization.Proto.Secp256k1(Key);
        }
    }

    public record Ed25519Key(string Key) : SignatureKey(SignatureTypeEnum.Ed25519)
    {
        public override Serialization.Json.PublicKey ToJson()
        {
            return new Serialization.Json.Ed25519(Key);
        }

        public override Serialization.Proto.SimplePublicKey ToProto()
        {
            return new Serialization.Proto.Ed25519(Key);
        }
    }

    public record MultisigKey(uint Threshold, SignatureKey[] Keys) : SignatureKey(SignatureTypeEnum.Multisig)
    {
        public override Serialization.Json.PublicKey ToJson()
        {
            return new Serialization.Json.MultisigKey(Threshold, Keys.Select(k => k.ToJson()).ToArray());
        }

        public override Serialization.Proto.SimplePublicKey ToProto()
        {
            return new Serialization.Proto.MultisigKey(Threshold, Keys.Select(k => k.ToProto()).ToArray());
        }
    }
}

namespace CosmosNetwork
{
    public abstract record SignatureKey(SignatureTypeEnum Type)
    {
        public abstract PublicKey ToPublicKey();

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
        public override PublicKey ToPublicKey()
        {
            return new PublicKey(this.Key);
        }

        public override Serialization.Json.PublicKey ToJson()
        {
            return new Serialization.Json.Secp256k1(this.Key);
        }

        public override Serialization.Proto.SimplePublicKey ToProto()
        {
            return new Serialization.Proto.Secp256k1(this.Key);
        }
    }

    public record Ed25519Key(string Key) : SignatureKey(SignatureTypeEnum.Ed25519)
    {
        public override PublicKey ToPublicKey()
        {
            return new PublicKey(this.Key);
        }

        public override Serialization.Json.PublicKey ToJson()
        {
            return new Serialization.Json.Ed25519(this.Key);
        }

        public override Serialization.Proto.SimplePublicKey ToProto()
        {
            return new Serialization.Proto.Ed25519(this.Key);
        }
    }

    public record MultisigKey(uint Threshold, SignatureKey[] Keys) : SignatureKey(SignatureTypeEnum.Multisig)
    {
        public override PublicKey ToPublicKey()
        {
            throw new NotImplementedException();
        }

        public override Serialization.Json.PublicKey ToJson()
        {
            return new Serialization.Json.MultisigKey(this.Threshold, this.Keys.Select(k => k.ToJson()).ToArray());
        }

        public override Serialization.Proto.SimplePublicKey ToProto()
        {
            return new Serialization.Proto.MultisigKey(this.Threshold, this.Keys.Select(k => k.ToProto()).ToArray());
        }
    }
}

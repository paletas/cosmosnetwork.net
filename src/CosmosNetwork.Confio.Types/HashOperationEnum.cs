namespace CosmosNetwork.Confio
{
    public enum HashOperationEnum
    {
        NoHash = Serialization.HashOperationEnum.NoHash,

        SHA256 = Serialization.HashOperationEnum.SHA256,

        SHA512 = Serialization.HashOperationEnum.SHA512,

        KECCAK = Serialization.HashOperationEnum.KECCAK,

        RIPEMD160 = Serialization.HashOperationEnum.RIPEMD160,

        BITCOIN = Serialization.HashOperationEnum.BITCOIN
    }
}

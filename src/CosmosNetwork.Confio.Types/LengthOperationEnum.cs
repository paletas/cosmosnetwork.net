namespace CosmosNetwork.Confio
{
    public enum LengthOperationEnum
    {
        NoPrefix = Serialization.LengthOperationEnum.NoPrefix,

        VarProto = Serialization.LengthOperationEnum.VarProto,

        VarRlp = Serialization.LengthOperationEnum.VarRlp,

        FixedBig32 = Serialization.LengthOperationEnum.FixedBig32,

        FixedLittle32 = Serialization.LengthOperationEnum.FixedLittle32,

        FixedBig64 = Serialization.LengthOperationEnum.FixedBig64,

        FixedLittle64 = Serialization.LengthOperationEnum.FixedLittle64,

        RequireBytes32 = Serialization.LengthOperationEnum.RequireBytes32,

        RequireBytes64 = Serialization.LengthOperationEnum.RequireBytes64
    }
}

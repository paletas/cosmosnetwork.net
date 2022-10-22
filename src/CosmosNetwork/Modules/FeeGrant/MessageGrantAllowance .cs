﻿using CosmosNetwork.Modules.FeeGrant.Allowances;
using CosmosNetwork.Serialization;

namespace CosmosNetwork.Modules.FeeGrant
{
    [CosmosMessage(COSMOS_DESCRIPTOR)]
    public record MessageGrantAllowance(
        CosmosAddress Granter,
        CosmosAddress Grantee,
        IAllowance Allowance) : Message
    {
        public const string COSMOS_DESCRIPTOR = "/cosmos.feegrant.v1beta1.MsgGrantAllowance";

        protected internal override SerializerMessage ToSerialization()
        {
            return new Serialization.MessageGrantAllowance(
                Granter.Address,
                Grantee.Address)
            {
                Allowance = Allowance.ToSerialization()
            };
        }
    }
}
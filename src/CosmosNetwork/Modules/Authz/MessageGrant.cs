﻿using CosmosNetwork.Serialization;

namespace CosmosNetwork.Modules.Authz
{
    [CosmosMessage(COSMOS_DESCRIPTOR)]
    public record MessageGrant(
        CosmosAddress Granter,
        CosmosAddress Grantee,
        Grant Grant) : Message
    {
        public const string COSMOS_DESCRIPTOR = "/cosmos.msgauth.v1beta1.MsgGrant";

        protected internal override SerializerMessage ToSerialization()
        {
            return new Serialization.MessageGrant(Granter.Address, Grantee.Address, Grant.ToSerialization());
        }
    }
}
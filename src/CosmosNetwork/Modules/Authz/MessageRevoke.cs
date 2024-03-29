﻿using CosmosNetwork.Serialization;

namespace CosmosNetwork.Modules.Authz
{
    [CosmosMessage(COSMOS_DESCRIPTOR)]
    public record MessageRevoke(
        CosmosAddress Granter,
        CosmosAddress Grantee,
        string MessageTypeUrl) : Message
    {
        public const string COSMOS_DESCRIPTOR = "/cosmos.msgauth.v1beta1.MsgRevoke";

        public override SerializerMessage ToSerialization()
        {
            return new Serialization.MessageRevoke(this.Granter.Address, this.Grantee.Address, this.MessageTypeUrl);
        }
    }
}
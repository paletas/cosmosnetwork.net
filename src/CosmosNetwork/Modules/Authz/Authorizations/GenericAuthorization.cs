﻿namespace CosmosNetwork.Modules.Authz.Authorizations
{
    public class GenericAuthorization : IAuthorization
    {
        public GenericAuthorization(string messageType)
        {
            this.MessageType = messageType;
        }

        public string MessageType { get; set; }

        public Serialization.Authorizations.IAuthorization ToSerialization()
        {
            return new Serialization.Authorizations.GenericAuthorization(this.MessageType);
        }
    }
}

﻿namespace CosmosNetwork.Modules.Authz.Serialization.Authorizations
{
    internal class GenericAuthorization : IAuthorization
    {
        public GenericAuthorization(string messageType)
        {
            TypeUrl = messageType;
        }

        public string TypeUrl { get; set; }

        public Authz.Authorizations.IAuthorization ToModel()
        {
            return new Authz.Authorizations.GenericAuthorization(TypeUrl);
        }
    }
}

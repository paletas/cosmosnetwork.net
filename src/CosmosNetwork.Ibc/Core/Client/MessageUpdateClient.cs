﻿using CosmosNetwork.Ibc.LightClients;
using CosmosNetwork.Serialization;

namespace CosmosNetwork.Ibc.Core.Client
{
    public record MessageUpdateClient(
        string ClientId,
        IHeader Header,
        string Signer) : Message
    {
        protected override SerializerMessage ToSerialization()
        {
            return new Serialization.Core.Client.MessageUpdateClient(this.ClientId, this.Signer)
            {
                Header = this.Header.ToSerialization()
            };
        }
    }
}

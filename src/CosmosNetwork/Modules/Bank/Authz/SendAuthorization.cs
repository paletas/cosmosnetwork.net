using CosmosNetwork.Modules.Authz.Authorizations;

namespace CosmosNetwork.Modules.Bank.Authz
{
    public class SendAuthorization : IAuthorization
    {
        public const string AuthorizationType = MessageSend.COSMOS_DESCRIPTOR;

        public string MessageType => SendAuthorization.AuthorizationType;

        public Coin[] SpendLimit { get; set; } = Array.Empty<Coin>();

        public Modules.Authz.Serialization.Authorizations.IAuthorization ToSerialization()
        {
            return new Serialization.Authz.SendAuthorization
            {
                SpendLimit = SpendLimit.Select(c => c.ToSerialization()).ToArray()
            };
        }
    }
}

namespace CosmosNetwork
{
    public record CosmosAddress(string Address)
    {
        public static implicit operator CosmosAddress(string address)
        {
            return new CosmosAddress(address);
        }

        public static implicit operator string(CosmosAddress address)
        {
            return address.Address;
        }

        public override string ToString()
        {
            return this.Address;
        }
    }
}

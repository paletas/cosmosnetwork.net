namespace Terra.NET
{
    public record TerraAddress(string Address)
    {
        public static implicit operator TerraAddress(string address)
        {
            return new TerraAddress(address);
        }

        public static implicit operator string(TerraAddress address)
        {
            return address.Address;
        }

        public override string ToString()
        {
            return this.Address;
        }
    }
}

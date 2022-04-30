namespace Terra.NET
{
    public record TerraAddress(string Address)
    {
        public static implicit operator TerraAddress(string address)
        {
            if (address is null) return null;
            return new TerraAddress(address);
        }

        public override string ToString()
        {
            return this.Address;
        }
    }
}

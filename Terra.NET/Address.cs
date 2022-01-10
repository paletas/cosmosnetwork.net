namespace Terra.NET
{
    public record TerraAddress(string Address)
    {
        public static implicit operator TerraAddress(string address)
        {
            return new TerraAddress(address);
        }

        public override string ToString()
        {
            return Address;
        }
    }
}

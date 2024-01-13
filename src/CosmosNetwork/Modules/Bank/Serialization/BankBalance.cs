using CosmosNetwork.Serialization;

namespace CosmosNetwork.Modules.Bank.Serialization
{
    internal class BankBalance
    {
        public string Address { get; set; }

        public DenomAmount[] Coins { get; set; }

        public Bank.BankBalance ToModel()
        {
            return new Bank.BankBalance(this.Address, this.Coins.Select(c => c.ToModel()).ToArray());
        }
    }
}

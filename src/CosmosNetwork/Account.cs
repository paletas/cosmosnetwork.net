﻿using System.Collections;

namespace CosmosNetwork
{
  public record AccountInformation(string AccountNumber, ulong AccountSequence);

    public class AccountBalances : IEnumerable<Coin>
    {
        private readonly IEnumerable<Coin> _balances;

        public AccountBalances(IEnumerable<Coin> balances)
        {
            this._balances = balances;
        }

        public UInt128? this[string denom] => this.SingleOrDefault(b => b.Denom == denom)?.Amount;

        public IEnumerator<Coin> GetEnumerator()
        {
            return this._balances.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this._balances.GetEnumerator();
        }
    }
}

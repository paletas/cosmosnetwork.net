using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosNetwork.Ibc.Core.Channel
{
    public record Channel(
        StateEnum State,
        OrderEnum Ordering,
        Counterparty Counterparty,
        string[] ConnectionHops,
        string Version)
    {
        internal Serialization.Core.Channel.Channel ToSerialization()
        {
            return new Serialization.Core.Channel.Channel(
                (Serialization.Core.Channel.StateEnum)this.State,
                (Serialization.Core.Channel.OrderEnum)this.Ordering,
                this.Counterparty.ToSerialization(),
                this.ConnectionHops,
                this.Version);
        }
    }
}

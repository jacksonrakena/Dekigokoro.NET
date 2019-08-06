using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dekigokoro.NET
{
    /// <summary>
    ///     A currency object returned by the API that represents currency that a player has.
    /// </summary>
    public class PlayerCurrency : PlayerBase
    {
        /// <summary>
        ///     The current balance of the player.
        /// </summary>
        [JsonProperty("balance", ItemConverterType = typeof(StringLongConverter))]
        public long Balance { get; set; }
    }
}

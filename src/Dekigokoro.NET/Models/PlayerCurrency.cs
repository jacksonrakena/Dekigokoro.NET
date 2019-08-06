using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dekigokoro.NET
{
    /// <summary>
    ///     A currency object returned by the API that represents currency that a player has.
    /// </summary>
    public class PlayerCurrency
    {
        /// <summary>
        ///     The ID of the player.
        /// </summary>
        [JsonProperty("player_id", ItemConverterType = typeof(StringLongConverter))]
        public long PlayerId { get; set; }

        /// <summary>
        ///     The current balance of the player.
        /// </summary>
        [JsonProperty("balance", ItemConverterType = typeof(StringLongConverter))]
        public long Balance { get; set; }

        /// <summary>
        ///     The subkey that was requested, if one was requested. Could be null.
        /// </summary>
        [JsonProperty("subkey")]
        public string Subkey { get; set; }
    }
}

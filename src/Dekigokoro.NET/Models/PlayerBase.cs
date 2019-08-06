using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dekigokoro.NET
{
    /// <summary>
    ///     Represents a data object containing information that is bound to a player.
    /// </summary>
    public abstract class PlayerBase : DekigoBase
    {
        /// <summary>
        ///     The ID of the player.
        /// </summary>
        [JsonProperty("player_id", ItemConverterType = typeof(StringLongConverter))]
        public long PlayerId { get; set; }
    }
}

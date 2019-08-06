using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dekigokoro.NET
{
    /// <summary>
    ///     An extension of <see cref="PlayerCurrency"/> that provides the user's rank within a ranking subset.
    /// </summary>
    public class PlayerCurrencyRanked : PlayerCurrency
    {
        /// <summary>
        ///     The player's rank within the ranking subset. 1 represents the first position, and so on.
        /// </summary>
        [JsonProperty("rank")]
        public long Rank { get; set; }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dekigokoro.NET
{
    /// <summary>
    ///     Represents a data object returned from the Dekigokoro API.
    /// </summary>
    public abstract class DekigoBase
    {
        /// <summary>
        ///     The subkey that was requested, if one was requested. Could be null.
        /// </summary>
        [JsonProperty("subkey")]
        public string Subkey { get; set; }
    }
}

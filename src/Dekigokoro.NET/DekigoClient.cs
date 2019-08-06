using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Dekigokoro.NET
{
    /// <summary>
    ///     Represents an HTTP client that can be used to interface with the Dekigokoro API.
    /// </summary>
    public class DekigoClient
    {
        private const string ApiBaseUrl = "https://dekigokoro.io/api/v1";

        internal HttpClient HttpClient { get; }

        private readonly string _token;
        private readonly DekigoClientOptions _clientOptions;

        /// <summary>
        ///     Creates a new <see cref="DekigoClient"/>.
        /// </summary>
        /// <param name="token">The API token to use when authorizing.</param>
        /// <param name="clientOptions">The client configuration.</param>
        public DekigoClient(string token, DekigoClientOptions clientOptions = null)
        {
            _token = token;
            _clientOptions = clientOptions ?? new DekigoClientOptions();

            if (string.IsNullOrWhiteSpace(_token)) throw new ArgumentException("Token must not be null, or whitespace.", nameof(token));

            HttpClient = new HttpClient();
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("", _token);
        }

        private async Task<string> InternalRequestAsync(Uri requestUri, HttpMethod method, HttpContent content = null)
        {
            if (content != null && content.Headers.ContentType.MediaType != "application/json" && method != HttpMethod.Get)
                throw new InvalidOperationException($"Cannot send a non-GET request with a Content-Type that is not application/json.");

            var message = new HttpRequestMessage
            {
                Method = method,
                RequestUri = requestUri,
                Content = content
            };

            var response = await HttpClient.SendAsync(message);

            response.EnsureSuccessStatusCode(); // I should probably do something about this, but this is good for now

            var contentString = await response.Content.ReadAsStringAsync();

            response.Dispose();
            message.Dispose();

            return contentString;
        }

        private async Task<T> RequestModelAsync<T>(string requestUri, HttpMethod method, object content = null)
        {
            var value = await InternalRequestAsync(new Uri(ApiBaseUrl + requestUri), method, new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");
            return JsonConvert.DeserializeObject<T>(value);
        }

        /// <summary>
        ///     Fetches the current currency balance for a player.
        /// </summary>
        /// <param name="playerId">The ID of the player.</param>
        /// <param name="subKey">The subkey to use. Optional.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous fetch operation.</returns>
        public Task<PlayerCurrency> GetPlayerCurrencyAsync(ulong playerId, string subKey = null)
        {
            if (subKey != null)
                return RequestModelAsync<PlayerCurrency>("/currency/" + playerId + "/" + subKey, HttpMethod.Get);

            return RequestModelAsync<PlayerCurrency>("/currency/" + playerId, HttpMethod.Get);
        }

        /// <summary>
        ///     Sets the currency balance for a player.
        /// </summary>
        /// <param name="playerId">The ID of the player.</param>
        /// <param name="newBalance">The new balance to set for the player.</param>
        /// <param name="subKey">The subkey to use. Optional.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous put operation.</returns>
        public Task<PlayerCurrency> SetPlayerCurrencyAsync(ulong playerId, long newBalance, string subKey = null)
        {
            var data = new
            {
                balance = newBalance.ToString()
            };

            if (subKey != null)
                return RequestModelAsync<PlayerCurrency>("/currency/" + playerId + "/" + subKey, HttpMethod.Put, data);

            return RequestModelAsync<PlayerCurrency>("/currency/" + playerId, HttpMethod.Put, data);
        }

        /// <summary>
        ///     Increments or decrements the currency balance for a player.
        /// </summary>
        /// <param name="playerId">The ID of the player.</param>
        /// <param name="increment">The amount to change the balance by. Could be negative, to decrement the player's balance.</param>
        /// <param name="subKey">The subkey to use. Optional.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous patch operation.</returns>
        public Task<PlayerCurrency> ChangePlayerCurrencyAsync(ulong playerId, long increment, string subKey = null)
        {
            var data = new
            {
                increment = increment.ToString()
            };

            if (subKey != null)
                return RequestModelAsync<PlayerCurrency>("/currency/" + playerId + "/" + subKey, new HttpMethod("PATCH") /* wtf .net? */, data);

            return RequestModelAsync<PlayerCurrency>("/currency/" + playerId, new HttpMethod("PATCH"), data);
        }
    }
}

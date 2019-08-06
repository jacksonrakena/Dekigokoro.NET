using System;
using System.Threading.Tasks;

namespace Dekigokoro.NET.Tests
{
    class Program
    {
        public static void Main(string[] args)
        {
            new Program().MainAsync().GetAwaiter().GetResult();
        }

        public async Task MainAsync()
        {
            var client = new DekigoClient(Environment.GetEnvironmentVariable("DekigoKey"));
            var leaderboard = await client.GetPlayerCurrencyLeaderboardAsync();

            foreach (var player in leaderboard)
            {
                Console.WriteLine("Rank " + player.Rank + ": " + "Player " + player.PlayerId + " has " + player.Balance + " coins.");
            }
        }
    }
}

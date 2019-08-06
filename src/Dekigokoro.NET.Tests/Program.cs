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
            Console.WriteLine(Environment.GetEnvironmentVariable("DekigoKey"));
            var client = new DekigoClient(Environment.GetEnvironmentVariable("DekigoKey"));

            await client.SetPlayerCurrencyAsync(1, 100);

            Console.WriteLine("Player 1 has " + (await client.GetPlayerCurrencyAsync(1)).Balance);
        }
    }
}

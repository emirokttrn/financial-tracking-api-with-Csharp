using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using FinancialTrackingAPI.Models;

namespace FinancialTrackingAPI.Service.Coin
{
    
          public class CoinGeckoService : ICoinGeckoService
    {
        private readonly HttpClient _httpClient;

        public CoinGeckoService(HttpClient httpClient)
{
    _httpClient = httpClient;
    _httpClient.DefaultRequestHeaders.Add("User-Agent", "FinancialTrackingAPI/1.0");
}

        public async Task<List<CoinMarket>> GetTopCoinsAsync(int count = 10)
        {
            var url = $"https://api.coingecko.com/api/v3/coins/markets?vs_currency=usd&order=market_cap_desc&per_page={count}&page=1";
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<CoinMarket>>(json) ?? new List<CoinMarket>();
        }

        public async Task<CoinMarket?> GetCoinBySymbolAsync(string symbol)
        {
            var coins = await GetTopCoinsAsync(100);
            return coins.FirstOrDefault(c => c.Symbol.ToLower() == symbol.ToLower());
        }
    }
    }

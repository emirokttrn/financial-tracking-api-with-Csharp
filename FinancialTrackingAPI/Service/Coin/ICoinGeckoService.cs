using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancialTrackingAPI.Models;

namespace FinancialTrackingAPI.Service
{
    public interface ICoinGeckoService
    {
        Task<List<CoinMarket>> GetTopCoinsAsync(int count = 10);
        Task<CoinMarket?> GetCoinBySymbolAsync(string symbol);
    }
}
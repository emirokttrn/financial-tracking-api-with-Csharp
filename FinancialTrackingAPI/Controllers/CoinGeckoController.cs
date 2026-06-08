using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancialTrackingAPI.Service;
using Microsoft.AspNetCore.Mvc;

namespace FinancialTrackingAPI.Controllers
{
       [ApiController]
    [Route("api/[controller]")]
    public class CoinGeckoController : ControllerBase
    {
        private readonly ICoinGeckoService _coinGeckoService;

        public CoinGeckoController(ICoinGeckoService coinGeckoService)
        {
            _coinGeckoService = coinGeckoService;
        }

        [HttpGet("top")]
        public async Task<IActionResult> GetTopCoins([FromQuery] int count = 10)
        {
            var coins = await _coinGeckoService.GetTopCoinsAsync(count);
            return Ok(coins);
        }

        [HttpGet("symbol/{symbol}")]
        public async Task<IActionResult> GetBySymbol([FromRoute] string symbol)
        {
            var coin = await _coinGeckoService.GetCoinBySymbolAsync(symbol);
            if (coin == null) return NotFound("Coin bulunamadı");
            return Ok(coin);
        }
    }
}
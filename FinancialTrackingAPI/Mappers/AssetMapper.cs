using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancialTrackingAPI.Dtos.Asset;
using FinancialTrackingAPI.Models;

namespace FinancialTrackingAPI.Mappers
{
    public static class AssetMapper
    {
        public static AssetResponse ToAssetResponse(this Asset asset)
        {
            return new AssetResponse
            {AssetId=asset.AssetId,
            Symbol = asset.Symbol,
            PortfolioId = asset.PortfolioId,
                AssetTpye = asset.AssetTpye,
                Quantity = asset.Quantity,
                BuyPrice = asset.BuyPrice,
                CurrentPrice = asset.CurrentPrice,
                Transactions = asset.Transactions.Select(x=>x.ToTransactionResponse()).ToList()// nedeni one to manyden dolayi kralinyo
            };
        }
        public static Asset ToAssentRequest(this AssetRequest request)
        {
            return new Asset
            {
                Symbol = request.Symbol,
                PortfolioId = request.PortfolioId,
                AssetTpye = request.AssetTpye,
                Quantity = request.Quantity,
                BuyPrice = request.BuyPrice,
                CurrentPrice = request.CurrentPrice

            };
        }
    }
}


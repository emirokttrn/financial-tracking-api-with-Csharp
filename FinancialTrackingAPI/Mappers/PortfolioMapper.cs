using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancialTrackingAPI.Dtos.Portfolio;
using FinancialTrackingAPI.Models;

namespace FinancialTrackingAPI.Mappers
{
    public static class PortfolioMapper
    {
        public static PortfolioResponse ToPortfolioResponse(this Portfolio portfolio)
        {
            return new PortfolioResponse
            {
                PortfolioId=portfolio.PortfolioId,
                 UserId=portfolio.UserId,
                Name=portfolio.Name,
                CreatedAt=portfolio.CreatedAt,
                TotalValue=portfolio.TotalValue,
                Assents=portfolio.Assents.Select(x=>x.ToAssetResponse()).ToList(),
                Transactions=portfolio.Transactions.Select(x=>x.ToTransactionResponse()).ToList()


            };
        }
        public static Portfolio ToPortfolioRequest(this PortfolioRequest request)
        {
            return new Portfolio
            {
                UserId=request.UserId,
                Name=request.Name,
                TotalValue=request.TotalValue
            };
        }
    }
}
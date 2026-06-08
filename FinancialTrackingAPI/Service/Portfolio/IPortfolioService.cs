using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancialTrackingAPI.Dtos.Portfolio;
using FinancialTrackingAPI.Helpers;

namespace FinancialTrackingAPI.Service.Portfolio
{
    public interface IPortfolioService
    {
        Task<List<PortfolioResponse>> PortfolioResponsesAsync(QueryObject query);
        Task<PortfolioResponse?> GetPortfolioResponseAsync(int id);
        Task<PortfolioResponse> CreatePortfolioResponseAsync(PortfolioRequest request);
        Task<PortfolioResponse?> UpdatePortfolioResponseAsync(int id, PortfolioRequest request);
        Task<bool> DeleteIsteAminakoyim(int id);
    }
}
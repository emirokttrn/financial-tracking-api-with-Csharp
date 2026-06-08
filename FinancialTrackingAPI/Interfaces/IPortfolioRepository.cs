using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancialTrackingAPI.Helpers;
using FinancialTrackingAPI.Models;

namespace FinancialTrackingAPI.Interfaces
{
    public interface IPortfolioRepository
    {
        Task<List<Portfolio>> GetPortfoliosAsync(QueryObject query);
        Task<Portfolio?> GetPortfolioByIdAsync(int id);

        Task<Portfolio> CreateAsync(Portfolio portfolio);

         Task<Portfolio?> UpdateAsync(int id,Portfolio portfolio);

         Task<bool> DeleteAsync(int id);

         Task<bool> PortfolioExists(int id);


    }
}
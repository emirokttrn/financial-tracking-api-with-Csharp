using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancialTrackingAPI.Helpers;
using FinancialTrackingAPI.Interfaces;
using FinancialTrackingAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FinancialTrackingAPI.Repositories
{
    
    public class PortfolioRepsository : IPortfolioRepository
    {
          private readonly AppDbContext _context;
        public PortfolioRepsository(AppDbContext context)
        {
            _context=context;
        }
        public async Task<Portfolio> CreateAsync(Portfolio portfolio)
        {
            await _context.Portfolios.AddAsync(portfolio);
            await _context.SaveChangesAsync();
            return portfolio;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.Portfolios.FirstOrDefaultAsync(x=>x.PortfolioId==id);
            if(existing==null) return false;
            _context.Remove(existing);
           await _context.SaveChangesAsync();
            return true;

        }

        public async Task<Portfolio?> GetPortfolioByIdAsync(int id)
        {
         return  await  _context.Portfolios
             .Include(x=>x.Assents)
           .ThenInclude(x=>x.Transactions)
           . FirstOrDefaultAsync(x=>x.PortfolioId==id);
           
        }

        public async Task<List<Portfolio>> GetPortfoliosAsync(QueryObject query)
        {
            var portfolios = _context.Portfolios
                .Include(x => x.Assents)
                .ThenInclude(x => x.Transactions)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                switch (query.SortBy.ToLower())
                {
                    case "name":
                        portfolios = query.IsDescending ? portfolios.OrderByDescending(x => x.Name) : portfolios.OrderBy(x => x.Name);
                        break;
                    case "totalvalue":
                        portfolios = query.IsDescending ? portfolios.OrderByDescending(x => x.TotalValue) : portfolios.OrderBy(x => x.TotalValue);
                        break;
                    default:
                        portfolios = portfolios.OrderBy(x => x.PortfolioId);
                        break;
                }
            }
            else
            {
                portfolios = portfolios.OrderBy(x => x.PortfolioId);
            }

            return await portfolios.Skip((query.PageNumber - 1) * query.PageSize).Take(query.PageSize).ToListAsync();
        }

        public async Task<bool> PortfolioExists(int id)
        {
        return await _context.Portfolios.AnyAsync(x=>x.PortfolioId==id);

        }

        public async Task<Portfolio?> UpdateAsync(int id, Portfolio portfolio)
        {
           var existing = await _context.Portfolios.FirstOrDefaultAsync(x => x.PortfolioId == id);
    if (existing == null) return null;

    existing.Name = portfolio.Name;
    existing.TotalValue = portfolio.TotalValue;

    await _context.SaveChangesAsync();
    return existing;
        }
    }
}
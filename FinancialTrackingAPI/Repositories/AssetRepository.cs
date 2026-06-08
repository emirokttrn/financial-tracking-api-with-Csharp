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
    public class AssetRepository : IAssetRepository
    {
        private readonly AppDbContext _context;
        public AssetRepository(AppDbContext context)
        {
            _context=context;
        }
        public async Task<bool> AssetExists(int id)
        {
            return await _context.Assets.AnyAsync(x=>x.AssetId==id);
        }

        public async Task<Asset> CreateAsync(Asset asset)
        {
            await _context.Assets.AddAsync(asset);
            await _context.SaveChangesAsync();
            return asset;
        }

        public async Task<bool> DeleteAsync(int id)
        {
             var existing = await _context.Assets.FirstOrDefaultAsync(x=>x.AssetId==id);
             if(existing==null) return false;
             _context.Remove(existing);
             await _context.SaveChangesAsync();
             return true;

        }

        public async Task<Asset?> GetAssetByIdAsync(int id)
        {
           return await _context.Assets.Include(t=>t.Transactions).FirstOrDefaultAsync(x=>x.AssetId==id);
        }

        public async Task<List<Asset>> GetAssetsAsync(QueryObject query)
        {
            var assets = _context.Assets.Include(x => x.Transactions).AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                switch (query.SortBy.ToLower())
                {
                    case "symbol":
                        assets = query.IsDescending ? assets.OrderByDescending(x => x.Symbol) : assets.OrderBy(x => x.Symbol);
                        break;
                    case "currentprice":
                        assets = query.IsDescending ? assets.OrderByDescending(x => x.CurrentPrice) : assets.OrderBy(x => x.CurrentPrice);
                        break;
                    default:
                        assets = assets.OrderBy(x => x.AssetId);
                        break;
                }
            }
            else
            {
                assets = assets.OrderBy(x => x.AssetId);
            }

            return await assets.Skip((query.PageNumber - 1) * query.PageSize).Take(query.PageSize).ToListAsync();
        }

        public async Task<Asset?> UpdateAsync(int id, Asset asset)
        {
        var assets=    await _context.Assets.FirstOrDefaultAsync(x=>x.AssetId==id);
            if(assets==null) return null;
            assets.Symbol = asset.Symbol;
    assets.AssetTpye = asset.AssetTpye;
    assets.Quantity = asset.Quantity;
    assets.BuyPrice = asset.BuyPrice;
    assets.CurrentPrice = asset.CurrentPrice;

    await _context.SaveChangesAsync();
    return assets;
        }
    }
}
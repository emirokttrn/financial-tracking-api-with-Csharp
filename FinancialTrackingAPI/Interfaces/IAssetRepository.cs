using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancialTrackingAPI.Helpers;
using FinancialTrackingAPI.Models;

namespace FinancialTrackingAPI.Interfaces
{
    public interface IAssetRepository
    {
        
        Task<List<Asset>> GetAssetsAsync(QueryObject query);
        Task<Asset?> GetAssetByIdAsync(int id);

        Task<Asset> CreateAsync(Asset asset);

        Task<Asset?> UpdateAsync(int id , Asset asset);

          Task<bool> DeleteAsync(int id);
        Task<bool> AssetExists(int id );
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancialTrackingAPI.Dtos.Asset;
using FinancialTrackingAPI.Helpers;

namespace FinancialTrackingAPI.Service.Asset
{
    public interface IAssetService
    {
        Task<List<AssetResponse>> GetAllAssetResponseasync(QueryObject query);
        Task<AssetResponse?> GetAssetResponseAsync(int id);
        Task<AssetResponse> CreateAssentResponseAsync(AssetRequest request);
        Task<AssetResponse?> UpdateAssetResponseAsync(int id, AssetRequest request);
        Task<bool> DeleteAsync(int id);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancialTrackingAPI.Dtos.Asset;
using FinancialTrackingAPI.Helpers;
using FinancialTrackingAPI.Interfaces;
using FinancialTrackingAPI.Mappers;

namespace FinancialTrackingAPI.Service.Asset
{
    public class AssetService : IAssetService
    {
private readonly IAssetRepository _repo;
public AssetService(IAssetRepository repo)
{
    _repo=repo;
}

public async Task<List<AssetResponse>> GetAllAssetResponseasync(QueryObject query)
        {
            var assets = await _repo.GetAssetsAsync(query);
            return assets.Select(x=>x.ToAssetResponse()).ToList();
        }

        public async Task<AssetResponse?>  GetAssetResponseAsync(int id)
        {
            var asset = await _repo.GetAssetByIdAsync(id);
            if(asset==null) return null;
            return asset.ToAssetResponse();
        }
        public async Task<AssetResponse> CreateAssentResponseAsync(AssetRequest request)
        {
           var createAsset= await _repo.CreateAsync(request.ToAssentRequest());
           return createAsset.ToAssetResponse();
        }
         public async Task<AssetResponse?> UpdateAssetResponseAsync(int id, AssetRequest request)
        {
         var existing=   await _repo.UpdateAsync(id,request.ToAssentRequest());
         if(existing==null) return null;
         return existing.ToAssetResponse();
        }
        public async Task<bool> DeleteAsync(int id)
        {
           return await _repo.DeleteAsync(id);
        }


    }
}
    // Task<List<AssetResponse>> GetAllAssetResponseasync(QueryObject query);
    //     Task<AssetResponse?> GetAssetResponseAsync(int id);
    //     Task<AssetResponse> CreateAssentResponseAsync(AssetRequest request);
    //     Task<AssetResponse?> UpdateAssetResponseAsync(int id, AssetRequest request);
    //     Task<bool> DeleteAsync(int id);
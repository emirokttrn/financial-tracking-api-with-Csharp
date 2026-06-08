using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancialTrackingAPI.Dtos.Portfolio;
using FinancialTrackingAPI.Helpers;
using FinancialTrackingAPI.Interfaces;
using FinancialTrackingAPI.Mappers;

namespace FinancialTrackingAPI.Service.Portfolio
{
    public class PortfolioService: IPortfolioService
    {
        private readonly IPortfolioRepository _repository;
        public PortfolioService(IPortfolioRepository repository)
        {
            _repository=repository;
        }

        public async Task<List<PortfolioResponse>> PortfolioResponsesAsync(QueryObject query)
        {
         var response= await _repository.GetPortfoliosAsync(query);
         return response.Select(x=>x.ToPortfolioResponse()).ToList();
        
        }
        public async Task<PortfolioResponse?> GetPortfolioResponseAsync(int id)
        {
         var existing=   await _repository.GetPortfolioByIdAsync(id);
         if(existing==null) return null;
         return existing.ToPortfolioResponse();
        }
        public async Task<PortfolioResponse> CreatePortfolioResponseAsync(PortfolioRequest request)
        {
          var create=  await _repository.CreateAsync(request.ToPortfolioRequest());
          return create.ToPortfolioResponse();
        }
        public async Task<PortfolioResponse?> UpdatePortfolioResponseAsync(int id, PortfolioRequest request)
        {
            var existing = await _repository.UpdateAsync(id,request.ToPortfolioRequest());
            if (existing == null) return null;
            return existing.ToPortfolioResponse();
        }
        public async Task<bool> DeleteIsteAminakoyim(int id)
        {
            return await _repository.DeleteAsync(id);
         
        }
    } 
}
//    Task<List<PortfolioResponse>> PortfolioResponsesAsync(QueryObject query);
//         Task<PortfolioResponse?> GetPortfolioResponseAsync(int id);
//         Task<PortfolioResponse> CreatePortfolioResponseAsync(PortfolioRequest request);
//         Task<PortfolioResponse?> UpdatePortfolioResponseAsync(int id, PortfolioRequest request);
//         Task<bool> DeleteIsteAminakoyim(int id);
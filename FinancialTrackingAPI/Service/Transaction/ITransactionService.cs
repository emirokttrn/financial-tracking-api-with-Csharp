using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancialTrackingAPI.Dtos.Transaction;
using FinancialTrackingAPI.Helpers;

namespace FinancialTrackingAPI.Service.Transaction
{
    public interface ITransactionService
    {
         Task<List<TransactionResponse>> GetAllAsync(QueryObject query);
        Task<TransactionResponse?> GetByIdAsync(int id);
        Task<TransactionResponse> CreateAsync(TransactionRequest request);
        Task<TransactionResponse?> UpdateAsync(int id, TransactionRequest request);
        Task<bool> DeleteAsync(int id);
    }
}
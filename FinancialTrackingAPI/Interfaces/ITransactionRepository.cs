using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancialTrackingAPI.Helpers;
using FinancialTrackingAPI.Models;

namespace FinancialTrackingAPI.Interfaces
{
    public interface ITransactionRepository
    {
        Task<List<Transaction>> GetTransactionAsync(QueryObject query);
        Task<Transaction?> GetTransactionByIdAsync(int id);

        Task<Transaction> CreateAsync(Transaction transaction);

        Task<Transaction?> UpdateAsync(int id , Transaction transaction);

        Task<bool> DeleteAsync(int id);
        Task<bool> TransactionExists(int id );
    }
}
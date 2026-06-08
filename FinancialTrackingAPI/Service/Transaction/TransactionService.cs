using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancialTrackingAPI.Dtos.Transaction;
using FinancialTrackingAPI.Helpers;
using FinancialTrackingAPI.Interfaces;
using FinancialTrackingAPI.Mappers;

namespace FinancialTrackingAPI.Service.Transaction
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _repository;

        public TransactionService(ITransactionRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<TransactionResponse>> GetAllAsync(QueryObject query)
        {
            var transactions = await _repository.GetTransactionAsync(query);
            return transactions.Select(x => x.ToTransactionResponse()).ToList();
        }

        public async Task<TransactionResponse?> GetByIdAsync(int id)
        {
            var transaction = await _repository.GetTransactionByIdAsync(id);
            if (transaction == null) return null;
            return transaction.ToTransactionResponse();
        }

        public async Task<TransactionResponse> CreateAsync(TransactionRequest request)
        {
            var transaction = await _repository.CreateAsync(request.toTransactionRequest());
            return transaction.ToTransactionResponse();
        }

        public async Task<TransactionResponse?> UpdateAsync(int id, TransactionRequest request)
        {
            var transaction = await _repository.UpdateAsync(id, request.toTransactionRequest());
            if (transaction == null) return null;
            return transaction.ToTransactionResponse();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
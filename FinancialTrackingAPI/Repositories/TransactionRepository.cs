using FinancialTrackingAPI.Helpers;
using FinancialTrackingAPI.Interfaces;
using FinancialTrackingAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FinancialTrackingAPI.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly AppDbContext _context;

        public TransactionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Transaction>> GetTransactionAsync(QueryObject query)
        {
            var transactions = _context.Transactions.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                switch (query.SortBy.ToLower())
                {
                    case "transactiondate":
                        transactions = query.IsDescending ? transactions.OrderByDescending(x => x.TransactionDate) : transactions.OrderBy(x => x.TransactionDate);
                        break;
                    case "buyprice":
                        transactions = query.IsDescending ? transactions.OrderByDescending(x => x.BuyPrice) : transactions.OrderBy(x => x.BuyPrice);
                        break;
                    default:
                        transactions = transactions.OrderBy(x => x.TransactionId);
                        break;
                }
            }
            else
            {
                transactions = transactions.OrderBy(x => x.TransactionId);
            }

            return await transactions.Skip((query.PageNumber - 1) * query.PageSize).Take(query.PageSize).ToListAsync();
        }

        public async Task<Transaction?> GetTransactionByIdAsync(int id)
        {
            return await _context.Transactions.FirstOrDefaultAsync(x => x.TransactionId == id);
        }

        public async Task<Transaction> CreateAsync(Transaction transaction)
        {
            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();
            return transaction;
        }

        public async Task<Transaction?> UpdateAsync(int id, Transaction transaction)
        {
            var existing = await _context.Transactions.FirstOrDefaultAsync(x => x.TransactionId == id);
            if (existing == null) return null;

            existing.AssentId = transaction.AssentId;
            existing.PortfolioId = transaction.PortfolioId;
            existing.TypeTransaction = transaction.TypeTransaction;
            existing.Quantity = transaction.Quantity;
            existing.BuyPrice = transaction.BuyPrice;
            existing.TransactionDate = transaction.TransactionDate;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.Transactions.FirstOrDefaultAsync(x => x.TransactionId == id);
            if (existing == null) return false;

            _context.Transactions.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> TransactionExists(int id)
        {
            return await _context.Transactions.AnyAsync(x => x.TransactionId == id);
        }

      
    }
}
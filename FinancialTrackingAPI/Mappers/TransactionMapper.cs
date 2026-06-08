using System;
using System.Collections.Generic;
using System.Linq;
using FinancialTrackingAPI.Dtos.Transaction;
using FinancialTrackingAPI.Models;

namespace FinancialTrackingAPI.Mappers
{
    public static class TransactionMapper
    {
        public static TransactionResponse ToTransactionResponse(this Transaction transaction)
        {
            return new TransactionResponse
            {
                TransactionId = transaction.TransactionId,
                PortfolioId = transaction.PortfolioId,
                AssentId = transaction.AssentId,
                TypeTransaction = transaction.TypeTransaction,
                Quantity = transaction.Quantity,
                BuyPrice = transaction.BuyPrice,
                TransactionDate = transaction.TransactionDate
            };
        }
        public static Transaction toTransactionRequest(this TransactionRequest request)
        {
            return new Transaction
            {
                PortfolioId = request.PortfolioId,
                AssentId = request.AssentId,
                TypeTransaction = request.TypeTransaction,
                Quantity = request.Quantity,
                BuyPrice = request.BuyPrice,
                TransactionDate = request.TransactionDate
            };



        }
    }
}
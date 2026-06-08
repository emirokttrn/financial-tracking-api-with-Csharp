using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancialTrackingAPI.Dtos.Asset;
using FinancialTrackingAPI.Dtos.Transaction;

namespace FinancialTrackingAPI.Dtos.Portfolio
{
    public class PortfolioResponse
    {
        
         public int PortfolioId { get; set; }

        public int? UserId { get; set; }             

        public string Name { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        public decimal TotalValue { get; set; }
        
        public List<AssetResponse> Assents { get; set; } = new List<AssetResponse>();
        public List<TransactionResponse> Transactions { get; set; } = new List<TransactionResponse>();
    }
}
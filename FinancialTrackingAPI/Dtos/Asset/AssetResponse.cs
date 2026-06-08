using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancialTrackingAPI.Dtos.Transaction;

namespace FinancialTrackingAPI.Dtos.Asset
{
    public class AssetResponse
    {
                public int AssetId { get; set; }
        public string Symbol {get; set;} =string.Empty;

        public int PortfolioId {get; set;}
   
    public string AssetTpye {get; set;}=string.Empty;
    
    public decimal Quantity {get; set;}

    public decimal BuyPrice {get; set;}

     
    public decimal CurrentPrice {get; set;}

    public DateTime CreatedAt{get; set;}

     public List<TransactionResponse> Transactions { get; set; } = new List<TransactionResponse>();


    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancialTrackingAPI.Dtos.Transaction
{
    public class TransactionResponse
    {
          public int TransactionId { get; set; }
        public int? PortfolioId {get; set;}

        public  int? AssentId{get; set;}

        public string TypeTransaction{get; set;}= string.Empty;

    public decimal Quantity {get; set;}

    public decimal BuyPrice {get; set;}
    
    public DateTime TransactionDate {get; set;}
    }
}
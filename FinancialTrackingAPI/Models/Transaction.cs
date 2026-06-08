using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinancialTrackingAPI.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public int? PortfolioId {get; set;}
        public Portfolio? Portfolio{get; set;}

        public  int? AssentId{get; set;}

        public Asset? Assent {get; set;}

        public string TypeTransaction{get; set;}= string.Empty;

            [Column(TypeName ="decimal(18,2)")]
    public decimal Quantity {get; set;}


        [Column(TypeName ="decimal(18,2)")]
    public decimal BuyPrice {get; set;}
    
    public DateTime TransactionDate {get; set;}
    }
}
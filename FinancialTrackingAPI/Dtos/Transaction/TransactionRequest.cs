using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FinancialTrackingAPI.Validtions;

namespace FinancialTrackingAPI.Dtos.Transaction
{
    public class TransactionRequest
    {[Required]
 [UnnecassaryCharacthers]
        public int? PortfolioId {get; set;}
        [Required]
        [UnnecassaryCharacthers]

        public  int? AssentId{get; set;}

[Required]
[MinLength(5,ErrorMessage ="5'den fazla olmali")]
        public string TypeTransaction{get; set;}= string.Empty;

    public decimal Quantity {get; set;}

    public decimal BuyPrice {get; set;}
    
    public DateTime TransactionDate {get; set;}
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinancialTrackingAPI.Models
{
    //assentin anlami onay o yuzzden bi assentin sadece bi portf. olur
    // o yuzden one to many many to one iliskisi var 
    public class Asset
    {
        public int AssetId { get; set; }
        public string Symbol {get; set;} =string.Empty;

        public int PortfolioId {get; set;}
        public Portfolio? Portfolio {get; set;}

    public string AssetTpye {get; set;}=string.Empty;
    
    [Column(TypeName ="decimal(18,2)")]
    public decimal Quantity {get; set;}

        [Column(TypeName ="decimal(18,2)")]
    public decimal BuyPrice {get; set;}

        [Column(TypeName ="decimal(18,2)")]
    public decimal CurrentPrice {get; set;}

    public DateTime CreatedAt{get; set;}

     public List<Transaction> Transactions { get; set; } = new List<Transaction>();




    }
}
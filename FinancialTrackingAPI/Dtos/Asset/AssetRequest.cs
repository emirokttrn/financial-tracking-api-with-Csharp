using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FinancialTrackingAPI.Validtions;

namespace FinancialTrackingAPI.Dtos.Asset
{
    public class AssetRequest
    {
        [Required]
        [MaxLength(100,ErrorMessage ="5'den fazla olmali")]
        public string Symbol {get; set;} =string.Empty;

[Required]
    [UnnecassaryCharacthers]
        public int PortfolioId {get; set;}

    public string AssetTpye {get; set;}=string.Empty;
    
    public decimal Quantity {get; set;}

    public decimal BuyPrice {get; set;}

    public decimal CurrentPrice {get; set;}




    }
}
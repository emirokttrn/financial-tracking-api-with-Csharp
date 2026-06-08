using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FinancialTrackingAPI.Validtions;

namespace FinancialTrackingAPI.Dtos.Portfolio
{
    public class PortfolioRequest
    {[Required]
    [UnnecassaryCharacthers]

         public int? UserId { get; set; } 
         [Required]
         [MinLength(5,ErrorMessage ="5'den fazla olmali")]
        public string Name { get; set; } = string.Empty;
    
        public decimal TotalValue { get; set; }
        
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancialTrackingAPI.Dtos.Portfolio;
using FinancialTrackingAPI.Models;

namespace FinancialTrackingAPI.Dtos.User
{
    public class UserResponse
    {
         public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
       
        public DateTime CreatedAt { get; set; }

    public List<PortfolioResponse> Portfolios { get; set; } = new List<PortfolioResponse>();
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FinancialTrackingAPI.Models
{
    public class Portfolio
    {
        public int PortfolioId { get; set; }

        public int? UserId { get; set; }        // foreign key
        public User? User { get; set; }          // navigation property

        public string Name { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalValue { get; set; }
        
        public List<Asset> Assents { get; set; } = new List<Asset>();
        public List<Transaction> Transactions { get; set; } = new List<Transaction>();

        

    }
}

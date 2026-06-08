using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FinancialTrackingAPI.Validtions;

namespace FinancialTrackingAPI.Dtos.User
{
    public class UserRequest
    {[Required]
    [UnnecassaryCharacthers]
    [MinLength(3,ErrorMessage ="5den fazla olmali")]
         public string UserName { get; set; } = string.Empty;
         [Required]
         [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
[MinLength(8,ErrorMessage ="5den fazla olmali")]
        public string PasswordHash { get; set; } = string.Empty;
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancialTrackingAPI.Helpers;
using FinancialTrackingAPI.Models;

namespace FinancialTrackingAPI.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetUsersAsync(QueryObject query);
        Task<User?> GetUserByIdAsync(int id);

        Task<User> CreateAsync(User user);

        Task<User?> UpdateAsync(int id , User user);

          Task<bool> DeleteAsync(int id);
        Task<bool> UserExists(int id );
        
    }
}
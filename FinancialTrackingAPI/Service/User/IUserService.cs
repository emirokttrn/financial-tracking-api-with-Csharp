using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancialTrackingAPI.Dtos.User;
using FinancialTrackingAPI.Helpers;

namespace FinancialTrackingAPI.Service.User
{
    public interface IUserService
    {
         Task<List<UserResponse>> GetAllAsync(QueryObject query);
        Task<UserResponse?> GetByIdAsync(int id);
        Task<UserResponse> CreateAsync(UserRequest request);
        Task<UserResponse?> UpdateAsync(int id, UserRequest request);
        Task<bool> DeleteAsync(int id);
        
    }
}
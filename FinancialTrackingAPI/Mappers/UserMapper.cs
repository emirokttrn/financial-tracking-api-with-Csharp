using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancialTrackingAPI.Dtos.User;
using FinancialTrackingAPI.Models;

namespace FinancialTrackingAPI.Mappers
{
    public static class UserMapper
    {
        public static UserResponse ToResponse(this User user)
        {
            return new UserResponse
            {
                UserId= user.UserId,
                UserName = user.UserName,
                Email=user.Email,
                CreatedAt=user.CreatedAt,
                Portfolios=user.Portfolios.Select(x=>x.ToPortfolioResponse()).ToList()
                
            };
        }
        public static User ToRequest(this UserRequest request)
        {
            return new User
            {
                UserName=request.UserName,
                Email=request.Email,
                PasswordHash=request.PasswordHash
                
            };
        }
    }
}
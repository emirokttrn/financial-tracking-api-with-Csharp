using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancialTrackingAPI.Dtos.User;
using FinancialTrackingAPI.Helpers;
using FinancialTrackingAPI.Interfaces;
using FinancialTrackingAPI.Mappers;

namespace FinancialTrackingAPI.Service.User
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<UserResponse>> GetAllAsync(QueryObject query)
        {
            var users = await _repository.GetUsersAsync(query);
            return users.Select(x => x.ToResponse()).ToList();
        }

        public async Task<UserResponse?> GetByIdAsync(int id)
        {
            var user = await _repository.GetUserByIdAsync(id);
            if (user == null) return null;
            return user.ToResponse();
        }

        public async Task<UserResponse> CreateAsync(UserRequest request)
        {
            var user = await _repository.CreateAsync(request.ToRequest());
            return user.ToResponse();
        }

        public async Task<UserResponse?> UpdateAsync(int id, UserRequest request)
        {
            var user = await _repository.UpdateAsync(id, request.ToRequest());
            if (user == null) return null;
            return user.ToResponse();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
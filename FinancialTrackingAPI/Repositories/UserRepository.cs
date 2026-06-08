using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancialTrackingAPI.Helpers;
using FinancialTrackingAPI.Interfaces;
using FinancialTrackingAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FinancialTrackingAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext appDbContext)
        {
            _context=appDbContext;
        }
        public async Task<User> CreateAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.Users.FirstOrDefaultAsync(x => x.UserId == id);
            if (existing == null) return false;

            _context.Users.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _context.Users
                .Include(x => x.Portfolios)
                .FirstOrDefaultAsync(x => x.UserId == id);
        }

        public async Task<List<User>> GetUsersAsync(QueryObject query)
        {
         var users = _context.Users.Include(x => x.Portfolios).AsQueryable();
         if(!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if(query.SortBy.Equals("username",StringComparison.OrdinalIgnoreCase))
                {
                   users= query.IsDescending
                   ? users.OrderByDescending(s=>s.UserName)
                   : users.OrderBy(s=>s.UserName);
                }
                if(query.SortBy.Equals("email",StringComparison.OrdinalIgnoreCase))
                {
                    users = query.IsDescending
                    ? users.OrderByDescending(s=>s.Email)
                    : users.OrderBy(s=>s.Email);
                }
            }
            var skipnumber= (query.PageNumber-1)*query.PageSize;
            return await users.Skip(skipnumber).Take(query.PageSize).ToListAsync();
        }

        public async Task<User?> UpdateAsync(int id, User user)
        {
            var existing = await _context.Users.FirstOrDefaultAsync(x => x.UserId == id);
            if (existing == null) return null;

            existing.UserName = user.UserName;
            existing.Email = user.Email;
            existing.PasswordHash = user.PasswordHash;

            await _context.SaveChangesAsync();
            return existing;
        }

        public Task<bool> UserExists(int id)
        {
            throw new NotImplementedException();
        }
    }
}
//    var users = _context.Users.Include(x => x.Portfolios).AsQueryable();

//             if (!string.IsNullOrWhiteSpace(query.SortBy))
//             {
//                 switch (query.SortBy.ToLower())
//                 {
//                     case "username":
//                         users = query.IsDescending ? users.OrderByDescending(x => x.UserName) : users.OrderBy(x => x.UserName);
//                         break;
//                     case "email":
//                         users = query.IsDescending ? users.OrderByDescending(x => x.Email) : users.OrderBy(x => x.Email);
//                         break;
//                     default:
//                         users = users.OrderBy(x => x.UserId);
//                         break;
//                 }
//             }
//             else
//             {
//                 users = users.OrderBy(x => x.UserId);
//             }

//             return await users.Skip((query.PageNumber - 1) * query.PageSize).Take(query.PageSize).ToListAsync();
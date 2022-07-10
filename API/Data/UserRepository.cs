using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<AppUser> GetUserByIdAsync(int id)
        {
            // Method to find a user by their Id
            return await _context.Users.FindAsync(id);
        }

        public async Task<AppUser> GetUserByUsernameAsync(string UserName)
        {
            // Method to find users by their username
            return await _context.Users.SingleOrDefaultAsync( x => x.UserName == UserName);
        }

        public async Task<IEnumerable<AppUser>> GetUsersAsync()
        {
            // Method to get the list of users
            return await _context.Users.ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
           // Method to save changes   
           return await _context.SaveChangesAsync() > 0;
        }

        public void Update(AppUser user)
        {
            // Flags the entity as modified in Entity Framework
            _context.Entry(user).State = EntityState.Modified;
        }
    }
}
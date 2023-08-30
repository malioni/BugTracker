using System;
using BugTracker.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using BugTracker.Data;
using BugTracker.Model;

namespace BugTracker.Services.Implementations
{
	public class UserRepo : IUserRepo
	{
        private readonly BugTrackerDbContext _context;
        public UserRepo(BugTrackerDbContext context)
        {
            _context = context;
        }

        public async Task Add(User user)
        {
            await _context.Users.AddAsync(user);
            await Save();
        }
        public async Task Update(User user)
        {
            _context.Update(user);
            await Save();
        }
        public async Task<User> GetByID(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> GetByName(string name)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserName == name);
        }

        private async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}


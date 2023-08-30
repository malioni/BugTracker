using System;
using BugTracker.Services.Interfaces;
using BugTracker.Data;
using BugTracker.Model;

namespace BugTracker.Services.Implementations
{
	public class BugRepo : IBugRepo
	{
		private readonly BugTrackerDbContext _context;
		public BugRepo(BugTrackerDbContext context)
		{
			_context = context;
		}

        public async Task Add(Bug bug)
		{
            await _context.Bugs.AddAsync(bug);
            await Save();
        }
        public async Task Update(Bug bug)
        {
            _context.Update(bug);
            await Save();
        }
        public async Task<Bug> GetByID(int id)
        {
            return await _context.Bugs.FindAsync(id);
        }

        public async Task<IEnumerable<Bug>> GetByUserID(int id)
        {
            return _context.Bugs.Where(b => b.UserID == id);
        }

        private async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}


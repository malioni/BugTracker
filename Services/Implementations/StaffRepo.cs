using System;
using BugTracker.Services.Interfaces;
using BugTracker.Data;
using Microsoft.EntityFrameworkCore;
using BugTracker.Model;

namespace BugTracker.Services.Implementations
{
    public class StaffRepo : IStaffRepo
    {
        private readonly BugTrackerDbContext _context;
        public StaffRepo(BugTrackerDbContext context)
        {
            _context = context;
        }

        public async Task Add(Staff staff)
        {
            await _context.Staff.AddAsync(staff);
            await Save();
        }
        public async Task Update(Staff staff)
        {
            _context.Update(staff);
            await Save();
        }
        public async Task<Staff> GetByID(int id)
        {
            return await _context.Staff.FindAsync(id);
        }

        public async Task<Staff> GetByName(string name)
        {
            return await _context.Staff.FirstOrDefaultAsync(s => s.StaffName == name);
        }

        private async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}



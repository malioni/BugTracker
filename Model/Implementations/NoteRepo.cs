using System;
using BugTracker.Model.Interfaces;
namespace BugTracker.Model.Implementations
{
    public class NoteRepo : INoteRepo
    {
        private readonly BugTrackerDbContext _context;
        public NoteRepo(BugTrackerDbContext context)
        {
            _context = context;
        }

        public async Task Add(Note note)
        {
            await _context.Notes.AddAsync(note);
            await Save();
        }
        public async Task Update(Note note)
        {
            _context.Update(note);
            await Save();
        }
        public async Task<Note> GetByID(int id)
        {
            return await _context.Notes.FindAsync(id);
        }

        private async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}

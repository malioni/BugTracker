using System;
using BugTracker.Model.Interfaces;
namespace BugTracker.Model.Implementations
{
	public class InteractionRepo : IInteractionRepo
	{
        private readonly BugTrackerDbContext _context;
        public InteractionRepo(BugTrackerDbContext context)
        {
            _context = context;
        }

        public async Task Add(Interaction interaction)
        {
            await _context.Interactions.AddAsync(interaction);
            await Save();
        }
        public async Task Update(Interaction interaction)
        {
            _context.Update(interaction);
            await Save();
        }
        public async Task<Interaction> GetByID(int id)
        {
            return await _context.Interactions.FindAsync(id);
        }

        private async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}


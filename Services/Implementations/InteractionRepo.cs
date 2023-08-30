using System;
using BugTracker.Services.Interfaces;
using BugTracker.Data;
using BugTracker.Model;

namespace BugTracker.Services.Implementations
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

        private async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}


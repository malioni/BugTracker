using System;
namespace BugTracker.Model.Interfaces
{
	public interface IInteractionRepo
	{
        Task Add(Interaction interaction);
        Task Update(Interaction interaction);
        Task<Interaction> GetByID(int id);
    }
}


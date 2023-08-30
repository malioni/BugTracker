using System;
using BugTracker.Model;
namespace BugTracker.Services.Interfaces
{
	public interface IInteractionRepo
	{
        Task Add(Interaction interaction);
    }
}


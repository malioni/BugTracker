using System;
using BugTracker.Model;
namespace BugTracker.Services.Interfaces
{
	public interface INoteRepo
	{
        Task Add(Note note);
        Task Update(Note note);
        Task<Note> GetByID(int id);
    }
}


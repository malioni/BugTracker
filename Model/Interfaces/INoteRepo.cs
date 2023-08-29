using System;
namespace BugTracker.Model.Interfaces
{
	public interface INoteRepo
	{
        Task Add(Note note);
        Task Update(Note note);
        Task<Note> GetByID(int id);
    }
}


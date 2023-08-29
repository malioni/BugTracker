using System;
namespace BugTracker.Model.Interfaces
{
	public interface IBugRepo
	{
        Task Add(Bug bug);
        Task Update(Bug bug);
        Task<Bug> GetByID(int id);
        Task<IEnumerable<Bug>> GetByUserID(int id);
    }
}


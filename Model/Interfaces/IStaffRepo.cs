using System;
namespace BugTracker.Model.Interfaces
{
	public interface IStaffRepo
	{
        Task Add(Staff staff);
        Task Update(Staff staff);
        Task<Staff> GetByID(int id);
        Task<Staff> GetByName(string name);
    }
}


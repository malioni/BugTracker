using System;
namespace BugTracker.Model.Interfaces
{
	public interface IUserRepo
	{
        Task Add(User user);
        Task Update(User user);
        Task<User> GetByID(int id);
        Task<User> GetByName(string name);
    }
}


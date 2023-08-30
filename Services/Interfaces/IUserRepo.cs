using System;
using BugTracker.Model;
namespace BugTracker.Services.Interfaces
{
	public interface IUserRepo
	{
        Task Add(User user);
        Task Update(User user);
        Task<User> GetByID(int id);
        Task<User> GetByName(string name);
    }
}


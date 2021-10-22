using System.Collections.Generic;
using VibeCheck.Models;

namespace VibeCheck.Repositories
{
    public interface IUserRepository
    {
        void Add(User user);
        User GetByFirebaseUserId(string firebaseUserId);
        List<User> GetAllUsers();
        

    }
}
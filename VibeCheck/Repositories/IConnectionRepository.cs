using System.Collections.Generic;
using VibeCheck.Models;

namespace VibeCheck.Repositories
{
    public interface IConnectionRepository
    {
        Connection GetConnectionById(int id);
        List<Connection> GetUsersConnections(int currentUser);
        void AddConnection(Connection connection);
        void UpdateConnection(Connection connection);
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using EVoteSystem.Models;

namespace EVoteSystem.Repositories
{
    public interface ISessionRepository : IRepository<VoteSession>
    {
        Task<IList<VoteSession>> GetActiveSessions();
        Task<VoteSession> FindSessionById(int? id);
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using EVoteSystem.Models;

namespace EVoteSystem.Repositories
{
    public interface IVoteRepository : IRepository<Vote>
    {
        Task<IList<Vote>> GetStudentVotes(string username);
        Task<IList<Vote>> GetCandidateVotes(int cid);
        Task<Vote> GetVoteById(int id);

        Task<IList<Vote>> GetStudentVoteByStudentIdAsync(int? id, int sessionId);
    }
}
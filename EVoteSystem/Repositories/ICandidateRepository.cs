using System.Collections.Generic;
using System.Threading.Tasks;
using EVoteSystem.Models;

namespace EVoteSystem.Repositories
{
    public interface ICandidateRepository : IRepository<Candidate>
    {
        Task<Candidate> FindCandidateById(int? cid);
        Task<Candidate> FindCandidateByStudentId(int? sid);
        Task<IList<Candidate>> FindCandidatesInSessionAsync(int? sessionId);
    }
}
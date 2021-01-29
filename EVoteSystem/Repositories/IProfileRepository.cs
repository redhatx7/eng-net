using System.Collections.Generic;
using System.Threading.Tasks;
using EVoteSystem.Models;

namespace EVoteSystem.Repositories
{
    public interface IProfileRepository : IRepository<Profile>
    {
        Task<IList<Profile>> GetCandidateProfileByIdAsync(int? candidateId);
        Task<Profile> FindProfileByIdAsync(int? id);
    }
}
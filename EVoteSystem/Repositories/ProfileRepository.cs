using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EVoteSystem.DatabaseContext;
using EVoteSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace EVoteSystem.Repositories
{
    public class ProfileRepository : Repository<Profile>, IProfileRepository
    {
        public ProfileRepository(EVoteDbContext context) : base(context)
        {
        }
        
        public async Task<IList<Profile>> GetCandidateProfileByIdAsync(int? candidateId)
        {
            return await _context.Set<Profile>().Where(x => x.Candidate.CandidateId == candidateId).AsNoTracking()
                .ToListAsync();
        }

        public async Task<Profile> FindProfileByIdAsync(int? id)
        {
            return await _context.Set<Profile>().Include(x => x.Candidate)
                .SingleOrDefaultAsync(x => x.ProfileId == id);
        }

    }
}